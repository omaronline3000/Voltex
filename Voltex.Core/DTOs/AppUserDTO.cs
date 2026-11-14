using System;
using System.Collections.Generic;
using System.Text;

namespace Voltex.Core.DTOs
{
    public class AppUserDTO
    {
        public string id { get; set; }
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string  Email { get; set; }
    }
    public class AddressDTO
    {
        public string City { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public string Street { get; set; }
    }
}
