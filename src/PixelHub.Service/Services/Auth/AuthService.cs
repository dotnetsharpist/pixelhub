using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PixelHub.DataAccess.IRepositories;
using PixelHub.Domain.Entities.User;
using PixelHub.Service.Exceptions;
using PixelHub.Service.Helpers;
using PixelHub.Service.Interfaces.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PixelHub.Service.Services.Auth;

public class AuthService : IAuthService
{
    private readonly IConfiguration configuration;
    private readonly IRepository<User> _repository;

    public AuthService(IConfiguration configuration, IRepository<User> repository)
    {
        this.configuration = configuration;
        _repository = repository;
    }

    public string GetUserIdFromToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        if (tokenHandler.CanReadToken(token))
        {
            var jwtToken = tokenHandler.ReadJwtToken(token);
            var idClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "Id");

            if (idClaim != null)
            {
                return idClaim.Value;
            }
        }

        return null;
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
