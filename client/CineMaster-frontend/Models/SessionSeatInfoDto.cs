namespace CineMaster_frontend.Models;

public class SessionSeatInfoDto
{
    public int SessionID { get; set; }
    public int Capacity { get; set; }
    public List<int> SoldSeats { get; set; } = new List<int>();
}
