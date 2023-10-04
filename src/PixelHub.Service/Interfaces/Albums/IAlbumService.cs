
using PixelHub.Domain.Configurations;
using PixelHub.Service.DTOs.Album;
using PixelHub.Service.DTOs.Image;

namespace PixelHub.Service.Interfaces.Albums;

public interface IAlbumService
{
    public Task<bool> DeleteAsync(long id);

    public Task<IEnumerable<ImageResultDto>> GetAllByAlbumIdAsync(long id, PaginationParams @params);

    public Task<AlbumResultDto> ModifyAsync(AlbumUpdateDto dto);

    public Task<AlbumResultDto> CreateAsync(AlbumCreateDto dto);
}
