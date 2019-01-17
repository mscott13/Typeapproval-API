using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebService.Database;
using WebService.Models;
using WebService.Commons;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;
namespace WebService.Controllers
{
    public class NotifyController : ApiController
    {

        [HttpPost]
        public HttpResponseMessage ForTypeApprovals([FromBody]Message message)
        {
            SLW_DatabaseInfo db = new SLW_DatabaseInfo();
            db.NotifySpecific(message.message, message.target_user, Constants.notifyTypeApproval);
            return Request.CreateResponse(HttpStatusCode.OK, "");
        }

        [HttpPost]
        public HttpResponseMessage ClientGeneral([FromBody]Message message)
        {
            return Request.CreateResponse(HttpStatusCode.OK, "");
        }

        [HttpPost]
        public HttpResponseMessage All([FromBody]Message message)
        {

            return Request.CreateResponse(HttpStatusCode.OK, "");
        }

        [HttpPost]
        public HttpResponseMessage SendEmail()
        {
            Execute().Wait();
            return Request.CreateResponse(HttpStatusCode.OK, "");
        }

        static async Task Execute()
        {
            Database.SLW_DatabaseInfo db = new Database.SLW_DatabaseInfo();
            Models.EmailSetting setting = db.GetEmailSetting();
            string password = Utilities.Encryption.Decrypt(setting.password);

            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("a.markscott13@gmail.com", "Example User");
            var subject = "Sending with SendGrid is Fun";
            var to = new EmailAddress("a.markscott13@yahoo.com", "Example User");
            var plainTextContent = "and easy to do anywhere, even with C#";
            var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}