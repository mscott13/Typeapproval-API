using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;

namespace WebService.Utilities
{
    public static class Email
    {
        public static void Send()
        {
            MailMessage mail = new MailMessage("you@yourcompany.com", "a.markscott13@gmail.com");
            SmtpClient client = new SmtpClient();
            client.Port = 25;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Host = "smtp.gmail.com";
            mail.Subject = "this is a test email.";
            mail.Body = "this is my test email body";
            client.Send(mail);

        }
    }
}