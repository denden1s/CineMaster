namespace CineMaster_frontend.Models;

public class CinemaSessionDto
{
    public int ID { get; set; }
    public DateTime ShowingTime { get; set; }
    public string FilmName { get; set; } = string.Empty;
    public string HallName { get; set; } = string.Empty;
    public int UserID { get; set; }
    public int SoldSeats { get; set; }
    public int RemainingSeats { get; set; }
}
