using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using PixelHub.DataAccess.IRepositories;
using PixelHub.Domain.Entities.User;
using PixelHub.Service.DTOs.Auth;
using PixelHub.Service.DTOs.Notifications;
using PixelHub.Service.DTOs.Security;
using PixelHub.Service.Exceptions;
using PixelHub.Service.Helpers;
using PixelHub.Service.Interfaces.Auth;
using PixelHub.Service.Interfaces.Notifications;
using System.Diagnostics.Eventing.Reader;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PixelHub.Service.Services.Auth;

public class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;
    private readonly IRepository<User> _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ITokenService _tokenService;
    private readonly IMemoryCache _memoryCache;
    private readonly IEmailSmsSender _emailSmsSender;

    private const int CACHED_MINUTES_FOR_REGISTER = 60;
    private const int CACHED_MINUTES_FOR_VERIFICATION = 5;
    private const string REGISTER_CACHE_KEY = "register_";
    private const string VERIFY_REGISTER_CACHE_KEY = "verify_register_";
    private const int VERIFICATION_MAXIMUM_ATTEMPTS = 3;

    public AuthService(IConfiguration configuration, IRepository<User> repository,
        IMapper mapper, IUnitOfWork unitOfWork, ITokenService tokenService,
        IEmailSmsSender smsSender)
    {
        _configuration = configuration;
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _tokenService = tokenService;
        _emailSmsSender = smsSender;
    }

    public async Task<(bool Result, string Token)> LoginAsync(LoginDto loginDto)
    {
        var user = await _unitOfWork.UserRepository.SelectAsync(x => x.Email == loginDto.Email);
        if (user is null)
            throw new NotFoundException("User not found!");

        var hasherResult = PasswordHasher.Verify(loginDto.Password, user.PasswordHash);
        if (!hasherResult)
            throw new CustomException(401,"Password is incorrect!");

        string token = await _tokenService.GenerateToken(user);

        return (Result: true, Token: token);
    }

    public async Task<(bool result, int CachedMinutes)> RegisterAsync(RegisterDto dto)
    {
        var exist = await _unitOfWork.UserRepository.SelectAsync(x => x.Email == dto.Email);
        if (exist is not null)
            throw new AlreadyExistException("User already exist!");

        if(_memoryCache.TryGetValue(REGISTER_CACHE_KEY + dto.Email, out RegisterDto cachedRegisterDto))
        {
            cachedRegisterDto.FirstName = dto.FirstName;
            _memoryCache.Remove(dto.Email);
        }

        else
        {
            _memoryCache.Set(REGISTER_CACHE_KEY + dto.Email, dto, TimeSpan.FromMinutes(CACHED_MINUTES_FOR_REGISTER));
        }

        return (Result: true, CachedMinutes: CACHED_MINUTES_FOR_REGISTER);
    }

    public async Task<(bool Result, int CachedVerificationMinutes)> SendCodeForRegisterAsync(string email)
    {
        var user = await _unitOfWork.UserRepository.SelectAsync(x => x.Email == email);
        if (user is not null) 
            throw new AlreadyExistException("User already exist!");

        if (_memoryCache.TryGetValue(REGISTER_CACHE_KEY + email, out RegisterDto registerDto))
        {
            VerificationDto verificationDto = new VerificationDto();
            verificationDto.Attempt = 0;
            verificationDto.CreatedAt = TimeHelper.GetDateTime();
            verificationDto.Code = CodeGenerator.GenerateRandomNumber();
            if (_memoryCache.TryGetValue(VERIFY_REGISTER_CACHE_KEY + email, out VerificationDto oldVerifcationDto))
            {
                _memoryCache.Remove(VERIFY_REGISTER_CACHE_KEY + email);
            }
            _memoryCache.Set(VERIFY_REGISTER_CACHE_KEY + email, verificationDto,
                TimeSpan.FromMinutes(CACHED_MINUTES_FOR_VERIFICATION));

            SmsMessage emailMessage = new SmsMessage();
            emailMessage.Title = "Heavy Service";
            emailMessage.Content = "Your verification code : " + verificationDto.Code;
            emailMessage.Recipient = email;
            var emailResult = await _emailSmsSender.SendAsync(emailMessage);
            if (emailResult is true) return (Result: true, CachedVerificationMinutes: CACHED_MINUTES_FOR_VERIFICATION);
            else return (Result: false, CachedVerificationMinutes: 0);
        }
        else throw new ExpiredExeption();
    }

    public async Task<(bool Result, string Token)> VerifyRegisterAsync(string email, int code)
    {
        if(_memoryCache.TryGetValue(REGISTER_CACHE_KEY + email, out RegisterDto registerDto))
        {
            if(_memoryCache.TryGetValue(VERIFY_REGISTER_CACHE_KEY + email, out VerificationDto verificationDto))
            {
                if (verificationDto.Attempt >= VERIFICATION_MAXIMUM_ATTEMPTS)
                    throw new CustomException(429, "Too many attempts please try again later.");
                else if (verificationDto.Code == code)
                {
                    var dbResult = true;
                    if (dbResult is true)
                    {
                        var user = await _unitOfWork.UserRepository.SelectAsync(x => x.Email == email);
                        string token = await _tokenService.GenerateToken(user);

                        return (Result: true, Token: token);
                    }

                    return (Result: dbResult, Token: "");
                }
                else
                {
                    _memoryCache.Remove(VERIFY_REGISTER_CACHE_KEY + email);
                    verificationDto.Attempt++;
                    _memoryCache.Set(VERIFY_REGISTER_CACHE_KEY + email, verificationDto,
                        TimeSpan.FromMinutes(CACHED_MINUTES_FOR_VERIFICATION));

                    return (Result: false, Token: "");
                }
            }
            else throw new VerificationTooManyRequestsException();
        }
        else throw new UserCacheDateExpiredExeption();
    }

    private async Task<bool> RegisterToDatabaseAsync(RegisterDto dto)
    {
        var user = new User();
        user.FirstName = dto.FirstName;
        user.LastName = dto.LastName;
        user.Email = dto.Email;
        user.Email_Confirmed = true;
        user.UserRole = Domain.Enums.UserRole.User;
        var hasherResult = PasswordHasher.Hash(dto.Password);
        user.PasswordHash = hasherResult;
        user.CreatedAt = user.UpdatedAt = TimeHelper.GetDateTime();
        var dbResult = await _unitOfWork.UserRepository.AddAsync(user);

        await _unitOfWork.SaveAsync();

        if (dbResult is not null)
            return true;
        return false;
    }
}
