using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Text;
using Voltex.Core.Interfaces;
using Voltex.Core.Sharing;
using Voltex.Infrastructure.Data.Migrations;
using MailKit.Security;
using MailKit;
namespace Voltex.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _settings;
        public EmailService(IOptions<EmailSettings> options)
        {
            _settings = options.Value;
        }
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Voltex Store", _settings.Email));
            message.Subject = subject;
            message.To.Add(MailboxAddress.Parse(email));
            message.Body = new BodyBuilder()
            {
                HtmlBody = htmlMessage
            }.ToMessageBody();
            using var client = new SmtpClient();
           await client.ConnectAsync(_settings.Host, _settings.Port,SecureSocketOptions.StartTls);
           await client.AuthenticateAsync(_settings.Email, _settings.Password);

            await client.SendAsync(message);
            await client.DisconnectAsync(true);

        }
    }
}
