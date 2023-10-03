using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using PixelHub.Service.Helpers;
using PixelHub.Service.Interfaces;

namespace PixelHub.Service.Services;

public class FileService : IFileService
{
    public readonly string MEDIA = "media";
    public readonly string IMAGES = "images";
    public readonly string ROOTPATH;

    public FileService(IWebHostEnvironment env)
    {
        ROOTPATH = env.WebRootPath;
    }

    public async Task<bool> DeleteImageAsync(string subpath)
    {
        string path = Path.Combine(ROOTPATH, subpath);

        if (File.Exists(path))
        {
            await Task.Run(() =>
            {
                File.Delete(path);
            }
            );
            return true;
        }
        else return false;
    }

    public async Task<string> UploadImageAsync(IFormFile image)
    {
        string newImageName = MediaHelper.MakeImageName(image.FileName);

        string subPath = Path.Combine(MEDIA, IMAGES, newImageName);
        string path = Path.Combine(ROOTPATH, subPath);

        var stream = new FileStream(path, FileMode.Create);
        await image.CopyToAsync(stream);
        stream.Close();

        return subPath;
    }
}
