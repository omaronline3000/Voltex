using System;
using System.Collections.Generic;
using System.Text;
using Voltex.Core.Entities;

namespace Voltex.Core.Interfaces
{
    public interface IGenerateTokenService
    {
        string CreateAndGetToken(AppUser user);
    }
}
