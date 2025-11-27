using ChristmasBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChristmasBackend.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Animation> Animations => Set<Animation>();
}
