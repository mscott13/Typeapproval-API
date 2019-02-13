using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;
using PostmarkDotNet;
using System.Threading.Tasks;



namespace WebService.Utilities
{
    public static class Email
    {

        public static void Send(string to_email, string subject, string message)
        {
            if (Commons.Constants.SEND_EMAIL)
            {
                Database.SLW_DatabaseInfo db = new Database.SLW_DatabaseInfo();
                Models.EmailSetting setting = db.GetEmailSetting();
                PostmarkSend(to_email, setting.email, subject, message, setting.company_name).ConfigureAwait(false);
            }        
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

        public static async Task PostmarkSend(string to_email, string from_email, string subject, string body, string tag)
        {
            var message = new PostmarkMessage()
            {
                To = to_email,
                From = from_email,
                TrackOpens = true,
                Subject = subject,
                TextBody = body,
                HtmlBody = "",
                Tag = tag
            };

            string key = FileManager.GetEmailKey();
            var client = new PostmarkClient(key);
            var sendResult = await client.SendMessageAsync(message);

            if (sendResult.Status == PostmarkStatus.Success)
            {
            }
            else
            {
            }
        }
    }
}