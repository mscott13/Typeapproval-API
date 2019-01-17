using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace WebService.Utilities
{
    public static class Email
    {
        public static bool Send(string to_email,string subject, string message)
        {
            Database.SLW_DatabaseInfo db = new Database.SLW_DatabaseInfo();
            try
            {
                Models.EmailSetting setting = db.GetEmailSetting();
                string password = Encryption.Decrypt(setting.password);

                MailMessage msg = new MailMessage(setting.email, to_email);
                msg.Body = message;
                msg.Subject = subject;
                msg.Priority = MailPriority.High;

                SmtpClient smtpClient = new SmtpClient();
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Host = "smtp.gmail.com";
                smtpClient.Port = 587;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.Credentials = new NetworkCredential(setting.email, password);
                smtpClient.EnableSsl = true;
                smtpClient.Send(msg);
                db.SaveActivity(new Models.UserActivity("", Commons.Constants.ACTIVITY_SET_EMAIL, "", "", 1));
                return true;
            }
            catch (Exception e)
            {
                db.SaveActivity(new Models.UserActivity("", Commons.Constants.ACTIVITY_ERROR, e.Message, "", 1));
                return false;
            }
        }

        public static void SendGrid(string to_email, string subject, string message)
        {
            Execute(to_email, subject, message).Wait();
        }

        static  async Task Execute(string to_email, string subject, string message)
        {
            Database.SLW_DatabaseInfo db = new Database.SLW_DatabaseInfo();
            Models.EmailSetting setting = db.GetEmailSetting();
            string password = Encryption.Decrypt(setting.password);

            var apiKey = "SG.vToJODFNTWqPxeHjJxnTwA.NvDKsQ8q6a8dFD3WFXAhchgBPQGpGJ890IHc4MAY5";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(setting.email, "Example User");
            var to = new EmailAddress("a.markscott13@gmail.com", "Example User");
            var plainTextContent = "and easy to do anywhere, even with C#";
            var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}