using Dotnet8.EmailService.Backend.Models;
using FluentEmail.Core;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet8.EmailService.Backend.Features;

[Route("api/[controller]")]
[ApiController]
public partial class EmailController : ControllerBase
{
    private readonly IFluentEmail _fluentEmail;

    public EmailController(IFluentEmail fluentEmail)
    {
        _fluentEmail = fluentEmail;
    }

    [HttpPost("send")]
    public async Task<IActionResult> SendEmail(EmailRequestModel requestModel)
    {
        var email = await _fluentEmail
            .To(requestModel.MailTo)
            .Subject(requestModel.Subject)
            .Body(requestModel.Body)
            .SendAsync();

        if (email.Successful)
        {
            return Ok(MessageResponseModel.Ok);
        }

        return StatusCode(500, MessageResponseModel.Fail);
    }
}
