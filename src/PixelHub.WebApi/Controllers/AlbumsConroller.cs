using Microsoft.AspNetCore.Mvc;
using PixelHub.Domain.Configurations;
using PixelHub.Service.DTOs.Album;
using PixelHub.Service.Interfaces.Albums;

namespace YourNamespace.Controllers
{
    [Route("api/albums")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        private readonly IAlbumService _albumService;

        public AlbumController(IAlbumService albumService)
        {
            _albumService = albumService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] AlbumCreateDto albumCreateDto) =>
            Ok(await _albumService.CreateAsync(albumCreateDto));

        [HttpGet("Album/{AlbumId}")]
        public async Task<IActionResult> GetAllByAlbumIdAsync(long albumId, [FromQuery] PaginationParams paginationParams) =>
            Ok(await _albumService.GetAllByAlbumIdAsync(albumId, paginationParams));

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(long id, [FromBody] AlbumUpdateDto albumUpdateDto) =>
            Ok(await _albumService.ModifyAsync(albumUpdateDto));

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(long id) =>
            Ok(await _albumService.DeleteAsync(id));
    }
}
