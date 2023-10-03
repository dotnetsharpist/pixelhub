namespace PixelHub.Service.DTOs.Album;

public class AlbumCreateDto
{
    public long UserId { get; set; }

    public string AlbumName { get; set; } = string.Empty;

}
