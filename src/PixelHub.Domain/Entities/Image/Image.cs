
using Microsoft.AspNetCore.Http;
using PixelHub.Domain.Commons;

namespace PixelHub.Domain.Entities.Image;

public class Image : Auditable
{
    public long UserId { get; set; }

    public long AlbumId { get; set; }

    public string ImageName { get; set; } = string.Empty;

    public string ImagePath { get; set; } = string.Empty;
}
