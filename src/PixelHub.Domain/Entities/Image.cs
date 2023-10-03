using PixelHub.Domain.Commons;

namespace PixelHub.Domain.Entities;

public class Image : Auditable
{
    public long UserId { get; set; }

    public long AlbumId { get; set; }

    public string ImagePath { get; set; } = string.Empty;
}
