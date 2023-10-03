namespace PixelHub.Service.Interfaces;

public interface IAuthService
{
    string GetUserIdFromToken(string token);

}
