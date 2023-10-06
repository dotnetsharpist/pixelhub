using Microsoft.AspNetCore.Mvc;
using PixelHub.Service.Interfaces.Auth;

namespace PixelHub.WebApi.Controllers;

[Route("api/auth")]
[ApiController]

public class AuthController : ControllerBase
{
    private readonly IAuthService authService;

    public AuthController(IAuthService service)
    {
        authService = service;
    }


}