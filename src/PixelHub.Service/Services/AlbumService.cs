using PixelHub.Service.DTOs.Album;
using PixelHub.Service.DTOs.User;
using PixelHub.Service.Interfaces.Albums;

namespace PixelHub.Service.Services;

public class AlbumService : IAlbumService
{
    public Task<AlbumResultDto> CreateAsync(AlbumCreateDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<AlbumResultDto> GetByUserIdAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<AlbumResultDto> ModifyAsync(AlbumUpdateDto dto)
    {
        throw new NotImplementedException();
    }
}
