using PixelHub.Service.Interfaces;
using System.IdentityModel.Tokens.Jwt;

namespace PixelHub.Service.Services;

public class AuthService : IAuthService
{
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
}
