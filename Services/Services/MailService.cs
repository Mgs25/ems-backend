using MailKit.Net.Smtp;
using MimeKit;

namespace ems_backend.Services;

public class MailService : IMailService
{
    private readonly IConfiguration _configuration;
    public MailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public void Send(string? toAddress, string? subject, string? body)
    {
        string SmtpServer = _configuration.GetSection("MailSettings:SmtpHost").Value;
        int Port = int.Parse(_configuration.GetSection("MailSettings:Port").Value);

        string fromAddress = _configuration.GetSection("MailSettings:MailAddress").Value;
        string Password = _configuration.GetSection("MailSettings:Password").Value;

        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(fromAddress));
        email.To.Add(MailboxAddress.Parse(toAddress));

        email.Subject = subject;
        
        email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = body };

        using (var smtp = new SmtpClient())
        {
            smtp.Connect(SmtpServer, Port);

            smtp.Authenticate(
                fromAddress,
                Password
            );

            smtp.Send(email);

            smtp.Disconnect(true);
        }
    }
}