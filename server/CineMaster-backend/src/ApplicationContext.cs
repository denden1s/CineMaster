using Microsoft.EntityFrameworkCore;
using CineMaster_backend.src.Entities;

namespace CineMaster_backend.src;

public class ApplicationContext : DbContext
{
  public DbSet<CinemaHall> CinemaHall { get; set; }
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

    modelBuilder.Entity<CinemaSession>()
      .HasOne<User>(s => s.Admin)
      .WithMany(u => u.Sessions)
      .HasForeignKey(s => s.UserID);

    modelBuilder.Entity<CinemaSession>()
      .HasOne<CinemaHall>(s => s.Hall)
      .WithMany(h => h.Sessions)
      .HasForeignKey(s => s.CinemaHallID);

    modelBuilder.Entity<CinemaSession>()
      .HasOne<Film>(s => s.ShowingFilm)
      .WithMany(f => f.Sessions)
      .HasForeignKey(s => s.FilmID);

    modelBuilder.Entity<Ticket>()
      .HasOne<CinemaSession>(t => t.Sessions)
      .WithMany(s => s.Tickets)
      .HasForeignKey(t => t.SessionID)
      .OnDelete(DeleteBehavior.NoAction);

    modelBuilder.Entity<Log>()
      .HasOne<User>(l => l.User)
      .WithMany(u => u.Logs)
      .HasForeignKey(l => l.UserID);

    modelBuilder.Entity<Film>()
      .HasOne<Genre>(f => f.Genre)
      .WithMany(g => g.Films)
      .HasForeignKey(f => f.GenreID);
  }
}
