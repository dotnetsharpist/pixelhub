using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PixelHub.DataAccess.IRepositories;
using PixelHub.Domain.Entities.User;
using PixelHub.Service.DTOs.Auth;
using PixelHub.Service.Exceptions;
using PixelHub.Service.Helpers;
using PixelHub.Service.Interfaces.Auth;
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

    private const int CACHED_MINUTES_FOR_REGISTER = 60;
    private const int CACHED_MINUTES_FOR_VERIFICATION = 5;
    private const string REGISTER_CACHE_KEY = "register_";
    private const string VERIFY_REGISTER_CACHE_KEY = "verify_register_";
    private const int VERIFICATION_MAXIMUM_ATTEMPTS = 3;

    public AuthService(IConfiguration configuration, IRepository<User> repository, IMapper mapper, IUnitOfWork unitOfWork, ITokenService tokenService)
    {
        _configuration = configuration;
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _tokenService = tokenService;
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


    }

    public Task<(bool Result, int CachedVerificationMinutes)> SendCodeForRegisterAsync(string phone)
    {
        throw new NotImplementedException();
    }

    public Task<(bool Result, string Token)> VerifyRegisterAsync(string phone, int code)
    {
        throw new NotImplementedException();
    }

    //public async Task<string> GenerateTokenAsync(string email, string password)
    //{
    //    var user = await _repository.SelectAsync(u => u.Email.Equals(email))
    //        ?? throw new NotFoundException("This user is not found");

    //    bool verifiedPassword = password.Verify(user.PasswordHash);
    //    if (!verifiedPassword)
    //        throw new CustomException(400, "Phone or password is invalid");

    //    var tokenHandler = new JwtSecurityTokenHandler();
    //    var tokenKey = Encoding.UTF8.GetBytes(configuration["JWT:Key"]);
    //    var tokenDescriptor = new SecurityTokenDescriptor
    //    {
    //        Subject = new ClaimsIdentity(new Claim[]
    //        {
    //             new Claim("Email", user.Email),
    //             new Claim("Id", user.Id.ToString()),
    //             new Claim(ClaimTypes.Role, user.UserRole.ToString())
    //        }),
    //        Expires = DateTime.UtcNow.AddHours(1),
    //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
    //    };
    //    var token = tokenHandler.CreateToken(tokenDescriptor);

    //    return tokenHandler.WriteToken(token);
    //}
}
