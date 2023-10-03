using PixelHub.Domain.Configurations;
using PixelHub.Service.DTOs.Image;

namespace PixelHub.Service.Interfaces.Images;

public interface IImageService
{
    public Task<bool> DeleteAsync(long Id);

    public Task<IEnumerable<ImageResultDto>> GetAllByUserIdAsync(long UserId, PaginationParams @params);

    public Task<ImageResultDto> ModifyAsync(ImageUpdateDto dto);

    public Task<ImageResultDto> CreateAsync(ImageCreateDto dto);

    public Task<ImageResultDto> GetByIdAsync(long imageId);
}
