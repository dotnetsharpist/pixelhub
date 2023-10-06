namespace PixelHub.Domain.Constants;

public static class TimeConstants
{

    public const int UTC = 5;

    public static DateTime GetCurrentTime()
    {
        var currentTime = DateTime.UtcNow.AddHours(5);
        return currentTime;
    }

    public static DateTime GetCurrentTime(int UTC)
    {
        var currentTime = DateTime.UtcNow.AddHours(UTC);
        return currentTime;
    }
}
