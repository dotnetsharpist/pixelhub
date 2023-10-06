namespace PixelHub.Service.DTOs.Notifications;

public class SmsMessage
{
    public string Recipient { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;
}
