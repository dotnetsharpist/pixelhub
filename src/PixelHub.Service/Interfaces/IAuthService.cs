namespace PixelHub.Service.Interfaces;

public interface IAuthService
{
    string GetUserIdFromToken(string token);
    Task<string> GenerateTokenAsync(string email, string password);
}
