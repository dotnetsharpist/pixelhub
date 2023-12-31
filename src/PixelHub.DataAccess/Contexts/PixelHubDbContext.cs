﻿using Microsoft.EntityFrameworkCore;
using PixelHub.Domain.Entities.Album;
using PixelHub.Domain.Entities.Image;
using PixelHub.Domain.Entities.User;

namespace PixelHub.DataAccess.Contexts;

public class PixelHubDbContext : DbContext
{
    public PixelHubDbContext(DbContextOptions<PixelHubDbContext> options) : base(options)
    { }

    public DbSet<User> users { get; set; }
    public DbSet<Album> albums { get; set; }
    public DbSet<Image> images { get; set; }
}
