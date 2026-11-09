using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Voltex.Core.Services
{
    public interface IImageManagementService
    {
        Task<List<string>> SaveImageAync(IFormFileCollection files, string src);
        void DeleteImageAsync(string src);
    }
}
