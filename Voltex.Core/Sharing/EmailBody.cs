using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace Voltex.Core.Sharing
{
    public static class EmailBody
    {
        public static string MailBody(string email , string token, string component, string message) => $""" 
            <h2>{message}</h2>
            <br>
            <a href='https://localhost:4200/account/{component}?email={email}&token={token}'>Click Here</a>
""";
    }
}
