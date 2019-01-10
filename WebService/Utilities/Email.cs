using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;

namespace WebService.Utilities
{
    public static class Email
    {
        public static void Send(string to_email,string subject, string message)
        {
            MailMessage msg = new MailMessage("Spectrum Management Authority, Jamaica", to_email);
            msg.Body = message;
            msg.Subject = subject;
            msg.Priority = MailPriority.High;

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.Port = 587;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.Credentials = new NetworkCredential("smaja.noreply@gmail.com", "spectrumharbour");
            smtpClient.EnableSsl = true;
            smtpClient.Send(msg);
        }
    }
}