using AutoMapper;
using PixelHub.DataAccess.IRepositories;
using PixelHub.Domain.Entities;
using PixelHub.Service.DTOs.User;
using PixelHub.Service.Helpers;
using PixelHub.Service.Interfaces.Users;
using PIxelHub.Service.Exceptions;
using System.Xml;

namespace PixelHub.Service.Services;
#pragma warning disable CS1998

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UserService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<UserResultDto> CreateAsync(UserCreateDto dto)
    {
        var exist = await _unitOfWork.UserRepository.SelectAsync(q => q.Email == dto.Email);

        if (exist is not null)
            throw new AlreadyExistException("This email was authorized before.");


        var newUser = _mapper.Map<User>(dto);
        dto.Password = PasswordHasher.Hash(dto.Password);
        newUser.PasswordHash = dto.Password;
        await _unitOfWork.UserRepository.AddAsync(newUser);
        await _unitOfWork.SaveAsync();

        return _mapper.Map<UserResultDto>(newUser);
    }

    public Task<bool> DeleteAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<UserResultDto>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<UserResultDto> GetByEmailAsync(string email)
    {
        throw new NotImplementedException();
    }

    public Task<UserResultDto> GetByIdAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<UserResultDto> ModifyAsync(UserUpdateDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<UserResultDto> ModifyPasswordAsync(long id, string oldPass, string newPass)
    {
        throw new NotImplementedException();
    }
}
