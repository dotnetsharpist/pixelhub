using PixelHub.Domain.Commons;

namespace PixelHub.Domain.Entities;

public class Album : Auditable
{ 
    public long UserId { get; set; }

    public string AlbumName { get; set; } = string.Empty;
}
