using Microsoft.AspNetCore.Mvc;
using PixelHub.Domain.Configurations;
using PixelHub.Service.DTOs.Image;
using PixelHub.Service.Interfaces.Images;

namespace YourNamespace.Controllers
{
    [Route("api/images")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;

        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] ImageCreateDto imageCreateDto) =>
            Ok(await _imageService.CreateAsync(imageCreateDto));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(long id) =>
            Ok(await _imageService.GetByIdAsync(id));

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetAllByUserIdAsync(long userId, [FromQuery] PaginationParams paginationParams) =>
            Ok(await _imageService.GetAllByUserIdAsync(userId, paginationParams));

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromBody] ImageUpdateDto imageUpdateDto) =>
            Ok(await _imageService.ModifyAsync(imageUpdateDto));

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(long id) =>
            Ok(await _imageService.DeleteAsync(id));
    }
}
