using System;
using System.Collections.Generic;
using System.Text;

namespace Voltex.Core.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string htmlMessage);
    }
}
