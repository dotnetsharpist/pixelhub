﻿
using PixelHub.Service.DTOs.Album;

namespace PixelHub.Service.Interfaces.Albums;

public interface IAlbumService
{
    public Task<bool> DeleteAsync(long id);

    public Task<AlbumResultDto> GetByUserIdAsync(long id);

    public Task<AlbumResultDto> ModifyAsync(AlbumUpdateDto dto);

    public Task<AlbumResultDto> CreateAsync(AlbumCreateDto dto);
}
