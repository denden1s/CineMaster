namespace CineMaster_backend.src.Entities;

public class Log
{
  public int ID { get; private set; }
  public int UserID { get; private set; }
  public string Action { get; private set; }
  public DateTime ActionTime { get; private set; }
  
  public Log() {}

  public Log(int userID, string action)
  {
    UserID = userID;
    Action = action;
  }
}
