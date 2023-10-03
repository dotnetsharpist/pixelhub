using AutoMapper;
using PixelHub.DataAccess.IRepositories;
using PixelHub.Domain.Configurations;
using PixelHub.Domain.Entities;
using PixelHub.Service.DTOs.Album;
using PixelHub.Service.DTOs.Image;
using PixelHub.Service.DTOs.User;
using PixelHub.Service.Exceptions;
using PixelHub.Service.Extensions;
using PixelHub.Service.Interfaces.Albums;

namespace PixelHub.Service.Services;

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

    public async Task<IEnumerable<AlbumResultDto>> GetAllByUserIdAsync(long id, PaginationParams @params)
    {
        var exist = await _unitOfWork.AlbumRepository.SelectAsync(x => x.UserId == id);

        if (exist is null)
            throw new NotFoundException("User not found!");

        var albums = _unitOfWork.ImageRepository.SelectAll(q => q.UserId == id).ToPaginate(@params);

        return _mapper.Map<IEnumerable<AlbumResultDto>>(albums);
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
