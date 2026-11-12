using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using Voltex.Core.DTOs;
using Voltex.Core.Entities;

namespace Voltex.Core.Interfaces
{
    public interface IAuthenticationRepository
    {
        Task<string> RegisterAsync(RegisterDTO info);
    }
}
