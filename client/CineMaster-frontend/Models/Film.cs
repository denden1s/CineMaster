namespace CineMaster_frontend.Models;

public class Film
{
    public int ID { get; set; }
    public int GenreID { get; set; }
    public string Name { get; set; }
    public int Duration { get; set; }
    public decimal Price { get; set; }
}
