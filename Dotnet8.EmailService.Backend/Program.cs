using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mailgun.Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Register Mailgun
builder.Services.AddSingleton<IMailgunClient>(serviceProvider =>
{
    var configuration = serviceProvider.GetRequiredService<IConfiguration>();
    var apiKey = configuration["Mailgun:ApiKey"];
    var domain = configuration["Mailgun:Domain"];
    return new MailgunClient(apiKey, domain);
});

// Add Swagger/OpenAPI support
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
