using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.InteropServices;
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
        public AuthenticationRepository(UserManager<AppUser> userManager , SignInManager<AppUser> signInManager, IEmailService emailService, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
            _configuration = configuration;
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
            var user = await _userManager.FindByEmailAsync(info.Email);
            if(user is null)
            {
                return "There is No Email, Register First";
            }
            if (!user.EmailConfirmed)
            {
                await SendConfirmationMail(user.Id, user.Email!, await _userManager.GenerateEmailConfirmationTokenAsync(user));
                return "Confirm Your Account First, We have send you an email";
            }
            var res = await _signInManager.CheckPasswordSignInAsync(user, info.Password,true);
            if (!res.Succeeded)
            {
                return "Password is Wrong";
            }
            await _signInManager.SignInAsync(user,true);
            // JWT token

            return "Done";
        }

        public async Task SendConfirmationMail(string userId, string Email, string token)
        {
            var encodedToken = WebUtility.UrlEncode(token);
            var confirmationLink = $"{_configuration["ClientSide:Anglur"]}/account/confirm-email?userId={userId}&token={encodedToken}";
            await _emailService.SendEmailAsync(Email, "Confirmation Mail", EmailBody.MailBody(confirmationLink, "Confirm Your Account"));
        }

    }
}