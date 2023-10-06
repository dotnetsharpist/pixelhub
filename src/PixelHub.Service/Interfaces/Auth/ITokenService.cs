using PixelHub.Domain.Entities;

namespace PixelHub.Service.Interfaces.Auth;

public interface ITokenService
{
    public string GenerateToken(User user);
}
