using AutoMapper;
using Microsoft.AspNetCore.Http;
using PixelHub.DataAccess.IRepositories;
using PixelHub.Domain.Entities;
using PixelHub.Service.DTOs.User;
using PixelHub.Service.Exceptions;
using PixelHub.Service.Helpers;
using PixelHub.Service.Interfaces.Users;
using PIxelHub.Service.Exceptions;
using System.Xml;

namespace PixelHub.Service.Services;
#pragma warning disable CS1998

public class UserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserService(IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _httpContextAccessor = httpContextAccessor;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<UserResultDto> CreateAsync(UserCreateDto dto)
    {
        var exist = await _unitOfWork.UserRepository.SelectAsync(q => q.Email == dto.Email);

        if (exist is not null)
            throw new AlreadyExistException("This email was authorized before.");


        var newUser = _mapper.Map<User>(dto);
        newUser.PasswordHash = PasswordHasher.Hash(dto.Password);
        await _unitOfWork.UserRepository.AddAsync(newUser);
        await _unitOfWork.SaveAsync();

        return _mapper.Map<UserResultDto>(newUser);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var user = await _unitOfWork.UserRepository.SelectAsync(q => q.Id == id);

        if (user is null)
            throw new NotFoundException("User not found");

        await _unitOfWork.UserRepository.DeleteAsync(x => x == user);

        return await _unitOfWork.SaveAsync();
    }

    public async Task<IEnumerable<UserResultDto>> GetAllAsync()
    {
        var users = _unitOfWork.UserRepository.SelectAll();

        return _mapper.Map<IEnumerable<UserResultDto>>(users);
    }

    public async Task<UserResultDto> GetByEmailAsync(string email)
    {
        var user = await _unitOfWork.UserRepository.SelectAsync(q => q.Email == email);

        if (user is null)
            throw new NotFoundException("User not found");

        return _mapper.Map<UserResultDto>(user);
    }

    public async Task<UserResultDto> GetByIdAsync(long id)
    {
        var user = await _unitOfWork.UserRepository.SelectAsync(q => q.Id == id);

        if (user is null)
            throw new NotFoundException("User not found");

        return _mapper.Map<UserResultDto>(user);
    }

    public async Task<UserResultDto> ModifyAsync(UserUpdateDto dto)
    {
        var exist = await _unitOfWork.UserRepository.SelectAsync(d => d.Id == dto.Id);

        if (exist is null)
            throw new NotFoundException("User not found");

        if (exist.Email != dto.Email)
        {
            var existUser = await _unitOfWork.UserRepository.SelectAsync(d => d.Email == dto.Email);
            if (existUser is not null)
                throw new AlreadyExistException("User already exist with this Email");
        }

        _mapper.Map(dto, exist);

        await _unitOfWork.UserRepository.UpdateAsync(exist);
        await _unitOfWork.SaveAsync();

        return _mapper.Map<UserResultDto>(exist);
    }

    public async Task<UserResultDto> ModifyPasswordAsync(long id, string oldPass, string newPass)
    {
        var exist = await _unitOfWork.UserRepository.SelectAsync(q => q.Id == id);

        if (exist is null)
            throw new NotFoundException("User not found");

        if (oldPass.Verify(exist.PasswordHash))
            throw new CustomException(403, "Passwor is invalid");

        exist.PasswordHash = newPass.Hash();
        await _unitOfWork.SaveAsync();

        return _mapper.Map<UserResultDto>(exist);
    }
}
