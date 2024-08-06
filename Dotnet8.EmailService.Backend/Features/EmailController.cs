using Microsoft.AspNetCore.Mvc;
using Mailgun.Core;
using Mailgun.Messages;
using System.Threading.Tasks;

namespace Dotnet8.EmailService.Backend.Features
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IMailgunClient _mailgunClient;

        public EmailController(IMailgunClient mailgunClient)
        {
            _mailgunClient = mailgunClient;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendEmail(EmailRequestModel requestModel)
        {
            var message = new MailgunMessage
            {
                From = "from@example.com",
                To = requestModel.MailTo,
                Subject = requestModel.Subject,
                Text = requestModel.Body,
                Html = requestModel.Body
            };

            var response = await _mailgunClient.SendEmailAsync(message);

            if (response.IsSuccessStatusCode)
            {
                return Ok(new { Message = "Email sent successfully" });
            }

            return StatusCode(500, new { Message = "Failed to send email" });
        }
    }

    public class EmailRequestModel
    {
        public string MailTo { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
