namespace CineMaster_backend.src.DTO;

public class SessionSeatDto
{
  public int SessionID { get; set; }
  public int Capacity { get; set; }
  public List<int> SoldSeats { get; set; } = new List<int>();

  public SessionSeatDto() { }

  public SessionSeatDto(int sessionId, int capacity, List<int> soldSeats)
  {
    SessionID = sessionId;
    Capacity = capacity;
    SoldSeats = soldSeats;
  }
}
