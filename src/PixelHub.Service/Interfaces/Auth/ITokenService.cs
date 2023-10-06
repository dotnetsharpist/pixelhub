using PixelHub.Domain.Entities.User;

namespace PixelHub.Service.Interfaces.Auth;

public interface ITokenService
{
    public string GenerateToken(User user);

    public string GetUserIdFromToken(string token);
}
