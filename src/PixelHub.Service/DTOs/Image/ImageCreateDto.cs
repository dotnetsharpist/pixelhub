using Microsoft.AspNetCore.Http;
using System.ComponentModel;

namespace PixelHub.Service.DTOs.Image;

public class ImageCreateDto
{
    public long UserId { get; set; }

    public long AlbumId { get; set; } = 1;

    public string ImageName { get; set; } = string.Empty;

    public IFormFile Content { get; set; }
}
