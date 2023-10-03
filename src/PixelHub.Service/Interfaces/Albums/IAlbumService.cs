using PixelHub.Service.DTOs.User;

namespace PixelHub.Service.Interfaces.Albums;

public interface IAlbumService
{
    public Task<bool> DeleteAsync(long id);

    public Task<UserResultDto> GetByUserIdAsync(long id);

    public Task<UserResultDto> ModifyAsync(UserUpdateDto dto);

    public Task<UserResultDto> CreateAsync(UserCreateDto dto);
}
