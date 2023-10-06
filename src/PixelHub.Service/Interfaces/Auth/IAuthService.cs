using PixelHub.Service.DTOs.Auth;

namespace PixelHub.Service.Interfaces.Auth;

public interface IAuthService
{
    public Task<(bool result, int CachedMinutes)> RegisterAsync(RegisterDto dto);

    public Task<>
}
