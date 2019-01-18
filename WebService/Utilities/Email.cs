using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;
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
    }
}