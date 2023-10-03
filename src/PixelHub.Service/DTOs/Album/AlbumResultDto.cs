namespace PixelHub.Service.DTOs.Album;

public class AlbumResultDto
{
    public long UserId { get; set; }

    public long AlbumId { get; set; }
    
    public string AlbumName { get; set; } = string.Empty;
}
