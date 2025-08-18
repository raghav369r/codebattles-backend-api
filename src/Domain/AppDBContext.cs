using CodeBattles_Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CodeBattles_Backend.Domain;

public class AppDBContext : DbContext
{
  public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }
  public DbSet<User> Users { get; set; }
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    var user = modelBuilder.Entity<User>();
    user.HasKey(e => e.Id);
    user.HasIndex(e => e.Email).IsUnique();
    user.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");
    user.Property(e => e.LastUpdatedAt).HasDefaultValueSql("GETDATE()");
    user.Property(e => e.LastSignIn).HasDefaultValueSql("GETDATE()");

    base.OnModelCreating(modelBuilder);
  }
}