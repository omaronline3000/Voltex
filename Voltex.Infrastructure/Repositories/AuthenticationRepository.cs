using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using Voltex.Core.Entities;
using Voltex.Core.Interfaces;

namespace Voltex.Infrastructure.Repositories
{
    public class Authentication : IAuthentication
    {
        private readonly UserManager<AppUser> _userManager;
        public Authentication(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<string> RegisterAsync()
        {
            
        }
    }
}
