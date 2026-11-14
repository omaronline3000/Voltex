using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;
using Voltex.Core.DTOs;
using Voltex.Core.Entities;
using Voltex.Core.Interfaces;
using Voltex.Core.Sharing;
using Voltex.Infrastructure.Data;
namespace Voltex.Infrastructure.Repositories
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly AppDbContext _context;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;
        private readonly IGenerateTokenService _generateToken;
        public AuthenticationRepository(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IEmailService emailService, IConfiguration configuration, IGenerateTokenService generateToken, AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
            _configuration = configuration;
            _generateToken = generateToken;
            _context = context;
        }
        public async Task<string> RegisterAsync(RegisterDTO info)
        {
  
            if(await _userManager.FindByNameAsync(info.UserName) is not null)
            {
                return "This UserName is already Exists";
            }

            if(await _userManager.FindByEmailAsync(info.Email) is not null)
            {
                return "This Email is already Exists";
            }

            AppUser user = new()
            {
                Email = info.Email,
                UserName = info.UserName,
                DisplayName = info.DisplayName,
                Address = new Address()
                {
                    City = info.Address.City,
                    Street = info.Address.Street,
                    ZipCode = info.Address.ZipCode,
                    Country = info.Address.Country
                }
            };
            var res = await _userManager.CreateAsync(user, info.Password);
            if (!res.Succeeded)
            {
                return res.Errors.ToList()[0].Description;
            }
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            await SendConfirmationMailAsync(user.Email,token);

            return "Done";
        }

        public async Task<string> LoginAsync(LoginDTO info)
        {
            var user = await _userManager.FindByEmailAsync(info.Email);
            if(user is null)
            {
                return "Please Register First";
            }
            if (!user.EmailConfirmed)
            {
                await SendConfirmationMailAsync(user.Email!, await _userManager.GenerateEmailConfirmationTokenAsync(user));
                return "Please confirm your account first, We have send you an email";
            }
            var res = await _signInManager.CheckPasswordSignInAsync(user, info.Password,true);
            if (!res.Succeeded)
            {
                return "Please enter valid email and password";
            }
            // JWT token
            return _generateToken.CreateAndGetToken(user);

        }

        public async Task SendConfirmationMailAsync(string Email, string token)
        {
            var encodedToken = WebUtility.UrlEncode(token);
            await _emailService.SendEmailAsync(Email, "Confirmation Mail", EmailBody.MailBody(Email , encodedToken , "confirm-mail", "Confirm Your Account"));
        }

        public async Task<bool> SendResetPasswordEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null)
            {
                return false;
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var encodedToken = WebUtility.UrlEncode(token);
            await _emailService.SendEmailAsync(user.Email!, "Reset Password", EmailBody.MailBody(email , encodedToken , "reset-password", "Reest Your Password"));
            return true;
        }

        public async Task<bool> ConfirmEmailAsync(ActiveEmailDTO emaildto)
        {
            var user = await _userManager.FindByEmailAsync(emaildto.Email);
            
            if(user is null)
            {
                return false;
            }

            var res = await _userManager.ConfirmEmailAsync(user, emaildto.Token);
            if (!res.Succeeded)
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                await SendConfirmationMailAsync(user.Email!, token);
            }
            return true;
        }

        
        public async Task<string> ResetPasswordAsync(ResetPasswordDTO resetpassword)
        {
            var user = await _userManager.FindByEmailAsync(resetpassword.Email);
            if(user is null)
            {
                return "This user is not exist";
            }
            var res = await _userManager.ResetPasswordAsync(user , resetpassword.Token , resetpassword.Password);
            if (res.Succeeded)
            {
                return "Password Reset Successfully";
            }
            else return res.Errors.ToList()[0].Description;
        }

        public async Task<bool> UpdateAddressAsync(string email, Address address)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null)
            {
                return false;
            }
            if (user.Address == address)
                _context.addresses.Update(address); 
            else await _context.addresses.AddAsync(address);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}