using CineMaster_backend.src.Entities;

namespace CineMaster_backend.src.DTO;

public class CinemaSessionDto
{
  public int ID { get; set; }
  public DateTime ShowingTime { get; set; }
  public string FilmName { get; set; }
  public string ImageUrl { get; set; }
  public string HallName { get; set; }
  public int UserID { get; set; }
  public int SoldSeats { get; set; }
  public int RemainingSeats { get; set; }

  public CinemaSessionDto() { }

  public CinemaSessionDto(CinemaSession session, int soldSeats, int remainingSeats)
  {
    ID = session.ID;
    ShowingTime = session.ShowingTime;
    FilmName = session.ShowingFilm?.Name ?? string.Empty;
    HallName = session.Hall?.Name ?? string.Empty;
    UserID = session.UserID;
    SoldSeats = soldSeats;
    RemainingSeats = remainingSeats;
    ImageUrl = session.ShowingFilm?.ImageUrl ?? string.Empty;
  }
}

public class TicketDto
{
  public int SessionID { get; set; }
  public int SitNumber { get; set; }

}
