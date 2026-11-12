using Microsoft.AspNetCore.Identity;
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

namespace Voltex.Infrastructure.Repositories
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;
        private readonly IGenerateToken _generateToken;
        public AuthenticationRepository(UserManager<AppUser> userManager , SignInManager<AppUser> signInManager, IEmailService emailService, IConfiguration configuration , IGenerateToken generateToken)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
            _configuration = configuration;
            _generateToken = generateToken;
        }
        public async Task<string> RegisterAsync(RegisterDTO info)
        {
            // if you will check for nullability in controller remove this part 
            if(info is null)
            {
                return null;
            }

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
                UserName = info.UserName
            };
            var res = await _userManager.CreateAsync(user, info.Password);
            if (!res.Succeeded)
            {
                return res.Errors.ToList()[0].Description;
            }
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            await SendConfirmationMail(user.Id,user.Email,token);

            return "Done";
        }

        public async Task<string> LoginAsync(LoginDTO info)
        {
            // if you will check for nullability in controller remove this part 
            if (info is null)
            {
                return null;
            }

            var user = await _userManager.FindByEmailAsync(info.Email);
            if(user is null)
            {
                return "There is No Email, Register First";
            }
            if (!user.EmailConfirmed)
            {
                await SendConfirmationMailAsync(user.Id, user.Email!, await _userManager.GenerateEmailConfirmationTokenAsync(user));
                return "Confirm Your Account First, We have send you an email";
            }
            var res = await _signInManager.CheckPasswordSignInAsync(user, info.Password,true);
            if (!res.Succeeded)
            {
                return "Email or Password is Wrong";
            }
            await _signInManager.SignInAsync(user,true);
            // JWT token
            return _generateToken.CreateAndGetToken(user);

        }

        public async Task SendConfirmationMailAsync(string userId, string Email, string token)
        {
            var encodedToken = WebUtility.UrlEncode(token);
            var confirmationLink = $"{_configuration["ClientSide:Confirmation Email Link"]}?userId={userId}&token={encodedToken}";
            await _emailService.SendEmailAsync(Email, "Confirmation Mail", EmailBody.MailBody(confirmationLink, "Confirm Your Account"));
        }

        public async Task<string> ConfirmEmail(string userId , string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            
            if(user is null)
            {
                return "There is no user with that email";
            }

            var res = await _userManager.ConfirmEmailAsync(user, token);
            if (!res.Succeeded)
            {
                return res.Errors.ToList()[0].Description;
            }
            return "Email Confirmed Successfully";

        }

        public async Task SendResetPasswordEmailAsync(AppUser user)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var encodedToken = WebUtility.UrlEncode(token);
            var confirmationLink = $"{_configuration["ClientSide:Confirmation Email Link"]}?userId={user.Id}&token={encodedToken}";
            await _emailService.SendEmailAsync(user.Email, "Reset Password", EmailBody.MailBody(confirmationLink, "Reest Your Password"));
        }

        public async Task<string> ResetPasswordAsync(AppUser user, string token, string NewPassword)
        {
            var res = await _userManager.ResetPasswordAsync(user, token, NewPassword);
            if (res.Succeeded)
            {
                return "Password Reset Successfully";
            }
            else return res.Errors.ToList()[0].Description;
        }
    }
}