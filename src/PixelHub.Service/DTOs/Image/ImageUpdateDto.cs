namespace PixelHub.Service.DTOs.Image;

public class ImageUpdateDto
{
    public long Id { get; set; }

    public long AlbumId { get; set; }

    public string ImageName { get; set; } = string.Empty;
}
