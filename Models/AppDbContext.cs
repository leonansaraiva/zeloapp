using Microsoft.EntityFrameworkCore;

namespace ZeloApp.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Escola> Escolas => Set<Escola>();
}