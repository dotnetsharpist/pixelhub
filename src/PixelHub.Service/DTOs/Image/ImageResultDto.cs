namespace PixelHub.Service.DTOs.Image;

public class ImageResultDto
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public string ImageName { get; set; } = string.Empty;

    public string ImagePath { get; set; } = string.Empty;
}
