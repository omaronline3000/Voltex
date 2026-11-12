using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

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
        
    }

}
