namespace CineMaster_frontend.Models;

public class SessionOption
{
    public CinemaSessionDto Session { get; set; }
    public string DisplayName { get; set; }

    public SessionOption(CinemaSessionDto session)
    {
        Session = session;
        DisplayName = $"{session.FilmName} — {session.ShowingTime:dd.MM.yyyy HH:mm} (Зал: {session.HallName})";
    }
}
