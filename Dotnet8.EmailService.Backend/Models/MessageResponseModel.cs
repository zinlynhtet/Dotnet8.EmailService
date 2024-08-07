namespace Dotnet8.EmailService.Backend.Models;

public static class MessageResponseModel
{
    public static readonly object Ok = new { Message = "Email sent successfully" };
    public static readonly object Fail = new { Message = "Failed to send email" };
}