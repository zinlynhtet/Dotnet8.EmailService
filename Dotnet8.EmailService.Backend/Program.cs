using System.Net.Mail;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var smtpSettings = builder.Configuration.GetSection("SmtpSettings").Get<SmtpSettings>();
builder.Services
           .AddFluentEmail(smtpSettings!.User)
           .AddRazorRenderer()
           .AddSmtpSender(new SmtpClient(smtpSettings.Host)
           {
               Port = smtpSettings.Port,
               Credentials = new NetworkCredential(smtpSettings.User, smtpSettings.Pass),
               EnableSsl = smtpSettings.EnableSsl
           });

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public class SmtpSettings
{
    public string Host { get; set; }
    public int Port { get; set; }
    public string User { get; set; }
    public string Pass { get; set; }
    public bool EnableSsl { get; set; }
}
