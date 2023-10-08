using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using MailKit.Security;
using PixelHub.Service.DTOs.Notifications;
using PixelHub.Service.Interfaces.Notifications;
using System.Net.Mail;

namespace PixelHub.Service.Services.Notifications;

public class EmailSmsSender : IEmailSmsSender
{
    private readonly string SENDER_EMAIL = string.Empty;
    private readonly string PLATFORM = string.Empty;
    private readonly string PASSWORD = string.Empty;
    private readonly int PORT;

    public EmailSmsSender(IConfiguration configuration)
    {
        SENDER_EMAIL = configuration["Email:SenderEmail"]!;
        PLATFORM = configuration["Email:Platform"]!;
        PASSWORD = configuration["Email:Password"]!;
        PORT = int.Parse(configuration["Email:Port"]!);
    }

    public async Task<bool> SendAsync(SmsMessage message)
    {
        try
        {
            var mail = new MimeMessage();
            mail.From.Add(MailboxAddress.Parse(SENDER_EMAIL));
            mail.To.Add(MailboxAddress.Parse(message.Recipient));
            mail.Subject = message.Title;
            mail.Body = new TextPart(TextFormat.Html)
            {
                Text = message.Content.ToString()
            };

            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            await smtp.ConnectAsync(PLATFORM, PORT, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(SENDER_EMAIL, PASSWORD);
            await smtp.SendAsync(mail);
            await smtp.DisconnectAsync(true);

            return true;
        }

        catch
        {
            return false;
        }
    }
}
