using Microsoft.EntityFrameworkCore;
using CineMaster_backend.src.Entities;

namespace CineMaster_backend.src;

public class ApplicationContext : DbContext
{
  public DbSet<CinemaSession> CinemaHall { get; set; }
  public DbSet<CinemaSession> CinemaSession { get; set; }
  public DbSet<Film> Film { get; set; }
  public DbSet<Genre> Genre { get; set; }
  public DbSet<Log> Log { get; set; }
  public DbSet<Ticket> Ticket { get; set; }
  public DbSet<User> User { get; set; }

  public ApplicationContext()
  {}
  public ApplicationContext(DbContextOptions<ApplicationContext> options)
    : base(options)
  {}

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<CinemaHall>().HasKey(h => h.ID);
    modelBuilder.Entity<CinemaSession>().HasKey(s => s.ID);
    modelBuilder.Entity<Film>().HasKey(f => f.ID);
    modelBuilder.Entity<Genre>().HasKey(g => g.ID);
    modelBuilder.Entity<Log>().HasKey(l => l.ID);
    modelBuilder.Entity<Ticket>().HasKey(t => t.ID);
    modelBuilder.Entity<User>().HasKey(u => u.ID);


    // TODO: like this
    // Set foreign keys
    // modelBuilder.Entity<Log>()
    //   .HasOne<User>(log => log.User)
    //   .WithMany(user => user.Logs)
    //   .HasForeignKey(log => log.User_ID);

    // modelBuilder.Entity<Price_list>()
    //   .HasOne<Car_model>(price => price.Model)
    //   .WithOne(model => model.Price)
    //   .HasForeignKey<Price_list>(price => price.Model_ID);

    // modelBuilder.Entity<Car>()
    //   .HasOne<Car_model>(car => car.Model)
    //   .WithMany(model => model.Cars)
    //   .HasForeignKey(car => car.Model_ID);
  }
}
