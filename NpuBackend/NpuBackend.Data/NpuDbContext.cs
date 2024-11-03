using Microsoft.EntityFrameworkCore;
using NpuBackend.Domain.Models;

public class NpuDbContext : DbContext
{
    public NpuDbContext(DbContextOptions<NpuDbContext> options) : base(options) { }

    public DbSet<NpuCreation> NpuCreations { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<NpuCreation>().HasKey(c => c.Id);
        modelBuilder.Entity<User>().HasKey(u => u.UserId);

        modelBuilder.Entity<NpuCreation>()
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(c => c.UserId);
    }
}