using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Voltex.Core.Entities;

namespace Voltex.Core.DTOs
{
    public record LoginDTO
    {
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
    public record RegisterDTO:LoginDTO
    {
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public AddressDTO Address { get; set; }
        
    }
    public record ResetPasswordDTO : LoginDTO
    {
        public string Token { get; set; }
    }
    public record ActiveEmailDTO
    {
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
