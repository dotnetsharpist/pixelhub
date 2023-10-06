using AutoMapper;
using PixelHub.DataAccess.IRepositories;
using PixelHub.Domain.Configurations;
using PixelHub.Domain.Entities.Album;
using PixelHub.Service.DTOs.Album;
using PixelHub.Service.DTOs.Image;
using PixelHub.Service.DTOs.User;
using PixelHub.Service.Exceptions;
using PixelHub.Service.Extensions;
using PixelHub.Service.Interfaces.Albums;

namespace PixelHub.Service.Services.Albums;

public class AlbumService : IAlbumService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AlbumService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<AlbumResultDto> CreateAsync(AlbumCreateDto dto)
    {
        var newAlbum = _mapper.Map<Album>(dto);

        await _unitOfWork.AlbumRepository.AddAsync(newAlbum);
        await _unitOfWork.SaveAsync();

        return _mapper.Map<AlbumResultDto>(newAlbum);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var exist = await _unitOfWork.AlbumRepository.SelectAsync(x => x.Id == id);

        if (exist is null)
            throw new NotFoundException("Album not found");

        await _unitOfWork.AlbumRepository.DeleteAsync(x => x == exist);

        return await _unitOfWork.SaveAsync();
    }

    public async Task<IEnumerable<ImageResultDto>> GetAllByAlbumIdAsync(long id, PaginationParams @params)
    {
        var exist = await _unitOfWork.AlbumRepository.SelectAsync(x => x.Id == id);

        if (exist is null)
            throw new NotFoundException("Album not found!");

        var images = _unitOfWork.ImageRepository.SelectAll(q => q.AlbumId == id).ToPaginate(@params);

        return _mapper.Map<IEnumerable<ImageResultDto>>(images);
    }

    public async Task<AlbumResultDto> ModifyAsync(AlbumUpdateDto dto)
    {
        var existAlbum = await _unitOfWork.AlbumRepository.SelectAsync(x => x.Id == dto.Id);

        if (existAlbum is null)
            throw new NotFoundException("Album not found");

        _mapper.Map(dto, existAlbum);
        await _unitOfWork.AlbumRepository.UpdateAsync(existAlbum);
        await _unitOfWork.SaveAsync();

        return _mapper.Map<AlbumResultDto>(existAlbum);
    }
}
