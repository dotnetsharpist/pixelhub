namespace PixelHub.Service.Interfaces.Auth;

public interface IAuthService
{
    string GetUserIdFromToken(string token);
    Task<string> GenerateTokenAsync(string email, string password);
}
