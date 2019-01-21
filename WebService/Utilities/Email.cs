using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;


namespace WebService.Utilities
{
    public static class Email
    {

        public static void Send(string to_email, string subject, string message)
        {
            Database.SLW_DatabaseInfo db = new Database.SLW_DatabaseInfo();
            Models.EmailSetting setting = db.GetEmailSetting();
            Execute(setting.email, setting.company_name, to_email, subject, message).Wait();
        }

        public static void SendEmailAdmins(string title, string message)
        {
            Database.SLW_DatabaseInfo db = new Database.SLW_DatabaseInfo();
            List<Models.UserDetails> userDetails = db.GetAllUsersDetails();

            for (int i = 0; i < userDetails.Count; i++)
            {
                if (userDetails[i].user_type == "Administrator")
                {
                    Send(userDetails[i].email, title, message);
                }
            }
        }

        static async Task Execute(string from_email, string company_name, string to_email, string subject, string message)
        {
            var apiKey = Environment.GetEnvironmentVariable("SendGrid", EnvironmentVariableTarget.Machine);
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(from_email, company_name);
            var to = new EmailAddress(to_email, "user");
            var msg = MailHelper.CreateSingleEmail(from, to, subject, message, message);
            var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
        }
    }
}