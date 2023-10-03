using Microsoft.AspNetCore.Mvc;
using PixelHub.Service.DTOs.User;
using PixelHub.Service.Interfaces.Users;
using System;
using System.Threading.Tasks;

[Route("api/users")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(UserCreateDto userDto)
        => Ok(await _userService.CreateAsync(userDto));
         

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
        => Ok(await _userService.GetAllAsync());
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(long id)
        => Ok(await _userService.GetByIdAsync(id));
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(UserUpdateDto userDto)
        => Ok(await _userService.ModifyAsync(userDto));

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(long id)
        => Ok(await _userService.DeleteAsync(id)); 
}
