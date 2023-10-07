using PixelHub.Service.DTOs.Notifications;

namespace PixelHub.Service.Interfaces.Notifications;

public interface IEmailSmsSender
{
    public Task<bool> SendAsync(SmsMessage message);
}
