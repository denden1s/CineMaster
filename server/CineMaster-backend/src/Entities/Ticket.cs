namespace CineMaster_backend.src.Entities;

public class Ticket
{
  public int ID { get; private set; }
  public int SessionID { get; private set; }
  public int UserID { get; private set; }
  public int SitNumber {get; private set; }
  public User User { get; set; }
  public CinemaSession Sessions { get; private set; }

  public Ticket() {}

  public Ticket(int sessionID, int userID, int sitNumber)
  {
    SessionID = sessionID;
    UserID = userID;
    SitNumber = sitNumber;
  }

  public Ticket(CinemaSession session, int sitNumber)
  {
    SessionID = session.ID;
    UserID = session.UserID;
    SitNumber = sitNumber;
  }
}
