namespace CineMaster_frontend.Models;

public class CinemaSession
{
    public int ID { get; set; }
    public int UserID { get; set; }
    public Film ShowingFilm { get; set; }
    public int FilmID { get; set; }
    public int CinemaHallID { get; set; }
    public DateTime ShowingTime { get; set; }
    public List<Ticket> Tickets { get; set; }
    public CinemaHall Hall { get; set; }
}

public class Ticket
{
    public int ID { get; set; }
    public int SessionID { get; set; }
    public int UserID { get; set; }
    public int SitNumber { get; set; }
}
