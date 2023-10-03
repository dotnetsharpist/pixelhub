using PixelHub.Domain.Entities;

namespace PixelHub.DataAccess.IRepositories;

public interface IUnitOfWork : IDisposable
{
    IRepository<User> UserRepository { get; }
    IRepository<Album> AlbumRepository { get; }
    IRepository<Image> ImageRepository { get; }
    Task<bool> SaveAsync();
}
