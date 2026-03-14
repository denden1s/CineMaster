namespace CineMaster_backend.src.Entities;

public class CinemaHall
{
  public int ID { get; private set; }
  public string Name { get; private set; }
  public int Capacity { get; private set; }
  public int VIPScalingPercent { get; private set; }
  public List<CinemaSession> Sessions { get; set; }

  public CinemaHall() {}

  public CinemaHall(string name, int capacity, int vipScalingPercent)
  {
    Name = name;
    Capacity = capacity;
    VIPScalingPercent = vipScalingPercent;
  }
}
