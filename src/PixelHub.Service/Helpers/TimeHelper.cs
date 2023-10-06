using PixelHub.Domain.Constants;

namespace PixelHub.Service.Helpers;


public class TimeHelper
{
    public static DateTime GetDateTime()
    {
            var dtTime = DateTime.Now;
            dtTime.AddHours(TimeConstants.UTC);
            return dtTime;
    }
}