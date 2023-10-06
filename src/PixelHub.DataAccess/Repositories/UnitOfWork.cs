using PixelHub.DataAccess.Contexts;
using PixelHub.DataAccess.IRepositories;
using PixelHub.Domain.Entities.Album;
using PixelHub.Domain.Entities.Image;
using PixelHub.Domain.Entities.User;

namespace PixelHub.DataAccess.Repositories;

public class UnitOfWork : IUnitOfWork
{

    private readonly PixelHubDbContext dbContext;

    public UnitOfWork(PixelHubDbContext dbContext)
    {
        this.dbContext = dbContext;
        UserRepository = new Repository<User>(dbContext);
        ImageRepository = new Repository<Image>(dbContext);
        AlbumRepository = new Repository<Album>(dbContext);
    }

    public IRepository<User> UserRepository { get; set; }
    public IRepository<Image> ImageRepository { get; set; }
    public IRepository<Album> AlbumRepository { get; set; }

    public void Dispose()
    {
        GC.SuppressFinalize(true);
    }

    public async Task<bool> SaveAsync()
    {
        var saved = await this.dbContext.SaveChangesAsync();
        return saved > 0;
    }
}
