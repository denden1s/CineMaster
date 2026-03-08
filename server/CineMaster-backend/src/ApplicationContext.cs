using Microsoft.EntityFrameworkCore;
using CineMaster_backend.src.Entities;

namespace CineMaster_backend.src;

public class ApplicationContext : DbContext
{
  public DbSet<User> User { get; set; }
  public ApplicationContext()
  {}
  public ApplicationContext(DbContextOptions<ApplicationContext> options)
    : base(options)
  {}

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<User>().HasKey(i => i.ID);
  }
}
