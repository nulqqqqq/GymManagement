using GymManagement.Api.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace GymManagement.Api.Services;

public class EmailService:IEmailService
{
    private readonly IConfiguration _config;

    public EmailService(IConfiguration  config)
    {
        _config = config;
    }
    public async Task SendWelcomeEmailAsync(string toEmail, string clientName)
    {
        var email = new MimeMessage();
        var senderName = _config["EmailSettings:SenderName"];
        var senderEmail = _config["EmailSettings:SenderEmail"];
        email.From.Add(new MailboxAddress(senderName, senderEmail));
        email.To.Add(new MailboxAddress(clientName, toEmail));
        email.Subject = "Welcome to Gym Management System! 💪";
        email.Body = new TextPart("html")
        {
            Text = $"<h3>Hello, {clientName}!</h3>" +
                   $"<p>We are thrilled to have you in our gym. Get ready to achieve your goals!</p>" +
                   $"<br><p>Best regards,<br>The Gym Team</p>"
        };
        
        using var smtp = new SmtpClient();
        try
        {
            var host = _config["EmailSettings:SmtpServer"];
            var port = int.Parse(_config["EmailSettings:SmtpPort"]!);
            var pass = _config["EmailSettings:Password"];
            await smtp.ConnectAsync(host, port, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(senderEmail, pass);
            await smtp.SendAsync(email);
        }
       finally
        {
            await smtp.DisconnectAsync(true);
        }
    }
}