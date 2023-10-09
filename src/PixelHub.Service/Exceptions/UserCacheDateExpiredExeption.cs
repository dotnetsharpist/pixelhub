namespace PixelHub.Service.Exceptions;

public class UserCacheDateExpiredExeption : ExpiredExeption
{
    public UserCacheDateExpiredExeption()
    {
        this.TitleMessage = "User data has expired!";
    }
}