using AutoMapper;
using PixelHub.Domain.Entities.Album;
using PixelHub.Domain.Entities.Image;
using PixelHub.Domain.Entities.User;
using PixelHub.Service.DTOs.Album;
using PixelHub.Service.DTOs.Image;
using PixelHub.Service.DTOs.User;

namespace PixelHub.Service.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // User
        CreateMap<UserCreateDto, User>().ReverseMap();
        CreateMap<UserUpdateDto, User>().ReverseMap();
        CreateMap<User, UserResultDto>().ReverseMap();

        // Image
        CreateMap<ImageCreateDto, Image>().ReverseMap();
        CreateMap<ImageUpdateDto, Image>().ReverseMap();
        CreateMap<Image, ImageResultDto>().ReverseMap();

        // Album
        CreateMap<AlbumCreateDto, Album>().ReverseMap();
        CreateMap<AlbumUpdateDto, Album>().ReverseMap();
        CreateMap<Album, AlbumResultDto>().ReverseMap();
    }
}
