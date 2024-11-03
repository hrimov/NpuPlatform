using Microsoft.EntityFrameworkCore;
using NpuBackend.Domain.Models;

public class NpuDbContext : DbContext
{
    public NpuDbContext(DbContextOptions<NpuDbContext> options) : base(options)
    {
    }

    public DbSet<NpuCreation> NpuCreations { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Score> Scores { get; set; }
    public DbSet<Element> Elements { get; set; }
    public DbSet<NpuCreationElement> NpuCreationElements { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<NpuCreation>()
            .HasKey(c => c.Id);
        
        modelBuilder.Entity<User>()
            .HasKey(u => u.UserId);
        
        modelBuilder.Entity<Element>()
            .HasKey(e => e.ElementId);
        
        modelBuilder.Entity<Score>()
            .HasKey(s => new { s.UserId, s.NpuCreationId });
        
        modelBuilder.Entity<Score>()
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(s => s.UserId);
        
        modelBuilder.Entity<Score>()
            .HasOne<NpuCreation>()
            .WithMany(c => c.Scores)
            .HasForeignKey(s => s.NpuCreationId);
    
        modelBuilder.Entity<NpuCreationElement>()
            .HasKey(nce => new { nce.NpuCreationId, nce.ElementId });
        
        modelBuilder.Entity<NpuCreationElement>()
            .HasOne(nce => nce.NpuCreation)
            .WithMany(nc => nc.Elements)
            .HasForeignKey(nce => nce.NpuCreationId);
        
        modelBuilder.Entity<NpuCreationElement>()
            .HasOne(nce => nce.Element)
            .WithMany()
            .HasForeignKey(nce => nce.ElementId);
    }
}
