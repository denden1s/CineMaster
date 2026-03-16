namespace CineMaster_backend.src.Entities;

public class CinemaSession
{
  public int ID { get; private set; }
  public int UserID { get; private set; }
  public User Admin { get; set; }
  public int FilmID { get; private set; }
  public int CinemaHallID { get; private set; }
  public DateTime ShowingTime { get; private set; }
  public List<Ticket> Tickets { get; set; }
  public CinemaHall Hall { get; set; }
  public Film ShowingFilm { get; set; }

  public CinemaSession() {}

  public CinemaSession(User user, Film film, CinemaHall cinemaHall,
                       DateTime showingTime)
  {
    UserID = user.ID;
    Admin = user;
    FilmID = film.ID;
    ShowingFilm = film;
    CinemaHallID = cinemaHall.ID;
    Hall = cinemaHall;
    ShowingTime = showingTime;
  }

  public CinemaSession(int userID, int filmID, int cinemaHallID,
                       DateTime showingTime)
  {
    UserID = userID;
    FilmID = filmID;
    CinemaHallID = cinemaHallID;
    ShowingTime = showingTime;
  }
}
