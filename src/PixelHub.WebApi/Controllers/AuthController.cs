using Microsoft.AspNetCore.Mvc;
using PixelHub.Service.Interfaces;

namespace PixelHub.WebApi.Controllers;

[Route("api/auth")]
[ApiController]

public class AuthController : ControllerBase
{
    private readonly IAuthService authService;


}