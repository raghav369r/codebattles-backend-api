using CodeBattles_Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CodeBattles_Backend.Domain;

public class AppDBContext : DbContext
{
  public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }
  public DbSet<User> Users { get; set; }
  public DbSet<Topic> Topics { get; set; }
  public DbSet<Problem> Problems { get; set; }
  public DbSet<ProblemTopic> ProblemTopics { get; set; }
  public DbSet<ExmapleTestCase> ExampleTestCases { get; set; }
  public DbSet<TestCase> TestCases { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    var user = modelBuilder.Entity<User>();
    user.HasKey(e => e.Id);
    user.HasIndex(e => e.Email).IsUnique();
    user.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");
    user.Property(e => e.LastUpdatedAt).HasDefaultValueSql("GETDATE()");
    user.Property(e => e.LastSignIn).HasDefaultValueSql("GETDATE()");

    var problem = modelBuilder.Entity<Problem>();
    problem.Property(p => p.Difficulty)
            .HasConversion<string>()
            .HasMaxLength(20);
    problem.Property(p => p.CreatedAt)
            .HasDefaultValueSql("GETDATE()");
            
    modelBuilder.Entity<ProblemTopic>()
         .HasKey(e => new { e.ProblemId, e.TopicId });

    base.OnModelCreating(modelBuilder);
  }
}