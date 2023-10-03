using Microsoft.AspNetCore.Http;

namespace PixelHub.Service.Interfaces;

public interface IFileService
{
    public Task<string> UploadImageAsync(IFormFile image);

    public Task<bool> DeleteImageAsync(string subpath);
}
