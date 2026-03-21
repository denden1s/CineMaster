namespace CineMaster_frontend.Models;

public class SessionDisplayItem
{
    public CinemaSessionDto Session { get; set; }
    public string FilmName => Session.FilmName;
    public DateTime ShowingTime => Session.ShowingTime;
    public string HallName => Session.HallName;
    public int SoldSeats => Session.SoldSeats;
    public int RemainingSeats => Session.RemainingSeats;
    public string ImageUrl => Session.ImageUrl;

    public SessionDisplayItem(CinemaSessionDto session)
    {
        Session = session;
    }
}
