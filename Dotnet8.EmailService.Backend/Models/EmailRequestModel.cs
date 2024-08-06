namespace Dotnet8.EmailService.Backend.Models
{
    public class EmailRequestModel
    {
        public string MailTo { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
