namespace CineMaster_backend.src.Entities;

public class Genre
{
  public int ID { get; private set; }
  public string Name { get; private set; }
  public List<Film> Films { get; set; }

  public Genre() {}

  public Genre(string name)
  {
    Name = name;
  }
}
