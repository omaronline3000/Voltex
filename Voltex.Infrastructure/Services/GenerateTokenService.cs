using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Voltex.Core.Entities;
using Voltex.Core.Interfaces;

namespace Voltex.Infrastructure.Services
{
    public class GenerateTokenService : IGenerateTokenService
    {
        private readonly IConfiguration _configuration;
        public GenerateTokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string CreateAndGetToken(AppUser user)
        {
            // Claims
            List<Claim> Claims = new();
            Claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
            Claims.Add(new Claim(ClaimTypes.Name, user.UserName!));
            // Claims.Add(new Claim(ClaimTypes.Role, "Guest"));
            Claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));


            // Verfication requirements
            SymmetricSecurityKey key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]!)
                );


            SigningCredentials cred = new SigningCredentials(
                algorithm: SecurityAlgorithms.HmacSha256,
                key: key
                );


            // Token Creation
            JwtSecurityToken jwt = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims: Claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: cred

                );

            // Generate and Return the Token
            var token = new JwtSecurityTokenHandler().WriteToken(jwt);
            return token;
        }
    }
}
