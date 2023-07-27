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
        => optionsBuilder.UseNpgsql("Host=dumbo.db.elephantsql.com;Database=zztqlrqx;Username=zztqlrqx;Password=wK0_-DVxIIqUAEfQriaJ-vDjJHd_VUT3", o => o.SetPostgresVersion(9, 6));

    public DbSet<User> Users { get; set; } = default!;
    public DbSet<Lists> TopTens { get; set; } = default!;
}