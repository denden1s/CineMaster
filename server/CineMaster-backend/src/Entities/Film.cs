namespace CineMaster_backend.src.Entities;

public class Film
{
  public int ID { get; private set; }
  public int GenreID { get; private set; }
  public string Name { get; private set; }
  public int Duration { get; private set; }
  public decimal Price { get; private set; }
  
  public Film() {}

  public Film(int genreID, string name, int duration, decimal price)
  {
    GenreID = genreID;
    Name = name;
    Duration = duration;
    Price = price;
  }
}
