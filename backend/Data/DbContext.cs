using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Data;

public class TopTenContext : DbContext
{
    public TopTenContext(DbContextOptions<TopTenContext> options)
     : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(o => o.SetPostgresVersion(9, 6));



    public DbSet<User> Users { get; set; } = default!;
    public DbSet<Lists> TopTens { get; set; } = default!;
}