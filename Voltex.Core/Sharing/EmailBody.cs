using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace Voltex.Core.Sharing
{
    public static class EmailBody
    {
        public static string MailBody(string link, string message) => $""" 
            <h2>{message}</h2>
            <br>
            <a href='{link}'>Click Here</a>
""";
    }
}
