using PixelHub.Service.DTOs.User;

namespace PixelHub.Service.Interfaces.Images;

public interface IImageService
{
    public Task<bool> DeleteAsync(long Id);

    public Task<UserResultDto> GetAllByUserIdAsync(long UserId);

    public Task<UserResultDto> ModifyAsync(UserUpdateDto dto);

    public Task<UserResultDto> CreateAsync(UserCreateDto dto);
}
