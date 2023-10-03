using PixelHub.Service.DTOs.User;

namespace PixelHub.Service.Interfaces.Users;

public interface IUserService
{
    public Task<bool> DeleteAsync(long id);

    public Task<UserResultDto> GetByIdAsync(long id);

    public Task<IEnumerable<UserResultDto>> GetAllAsync();

    public Task<UserResultDto> GetByEmailAsync(string email);

    public Task<UserResultDto> ModifyAsync(UserUpdateDto dto);

    public Task<UserResultDto> CreateAsync(UserCreateDto dto);

    public Task<UserResultDto> ModifyPasswordAsync(long id, string oldPass, string newPass);
}
