using Microsoft.Extensions.Configuration;
using MimeKit;
using PixelHub.Service.DTOs.Notifications;
using PixelHub.Service.Interfaces.Notifications;

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
        }

        catch
        {

        }
    }
}
