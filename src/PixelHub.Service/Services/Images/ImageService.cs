using AutoMapper;
using Microsoft.AspNetCore.Http;
using PixelHub.DataAccess.IRepositories;
using PixelHub.Domain.Configurations;
using PixelHub.Domain.Entities.Image;
using PixelHub.Service.DTOs.Image;
using PixelHub.Service.DTOs.User;
using PixelHub.Service.Exceptions;
using PixelHub.Service.Extensions;
using PixelHub.Service.Interfaces;
using PixelHub.Service.Interfaces.Images;

namespace PixelHub.Service.Services.Images;

public class ImageService : IImageService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileService _fileService;
    private readonly IMapper _mapper;

    public ImageService(IUnitOfWork unitOfWork, IMapper mapper, IFileService fileService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _fileService = fileService;
    }

    public async Task<ImageResultDto> CreateAsync(ImageCreateDto dto)
    {
        var newImage = _mapper.Map<Image>(dto);
        var result = await _fileService.UploadImageAsync(dto.Content);

        var album = await _unitOfWork.AlbumRepository.SelectAsync(q => q.Id == dto.AlbumId);

        if (album is null)
            throw new NotFoundException("Album not found");

        newImage.ImagePath = result;

        await _unitOfWork.ImageRepository.AddAsync(newImage);
        await _unitOfWork.SaveAsync();

        return _mapper.Map<ImageResultDto>(newImage);
    }

    public async Task<bool> DeleteAsync(long Id)
    {
        var image = await _unitOfWork.ImageRepository.SelectAsync(q => q.Id == Id);

        if (image is null)
            throw new NotFoundException("Image not found");

        var result = await _fileService.DeleteImageAsync(image.ImagePath);

        return result;
    }

    public async Task<IEnumerable<ImageResultDto>> GetAllByUserIdAsync(long UserId, PaginationParams @params)
    {
        var images = _unitOfWork.ImageRepository.SelectAll(q => q.UserId == UserId).ToPaginate(@params);

        return _mapper.Map<IEnumerable<ImageResultDto>>(images);
    }

    public async Task<ImageResultDto> GetByIdAsync(long imageId)
    {
        var image = await _unitOfWork.ImageRepository.SelectAsync(q => q.Id == imageId);

        if (image is null)
            throw new NotFoundException("Image not found");

        return _mapper.Map<ImageResultDto>(image);
    }

    public async Task<ImageResultDto> ModifyAsync(ImageUpdateDto dto)
    {
        var existImage = await _unitOfWork.ImageRepository.SelectAsync(x => x.Id == dto.Id);

        if (existImage is null)
            throw new NotFoundException("Image not found");

        _mapper.Map(dto, existImage);
        await _unitOfWork.ImageRepository.UpdateAsync(existImage);
        await _unitOfWork.SaveAsync();

        return _mapper.Map<ImageResultDto>(existImage);
    }
}
