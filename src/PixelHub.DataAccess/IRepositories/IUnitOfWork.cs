using PixelHub.Domain.Entities.Album;
using PixelHub.Domain.Entities.Image;
using PixelHub.Domain.Entities.User;

namespace PixelHub.DataAccess.IRepositories;

public interface IUnitOfWork : IDisposable
{
    IRepository<User> UserRepository { get; }
    IRepository<Album> AlbumRepository { get; }
    IRepository<Image> ImageRepository { get; }
    Task<bool> SaveAsync();
}
