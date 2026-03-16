using CineMaster_backend.src.Entities;

namespace CineMaster_backend.src.Services;

public class CinemaSessionService
{
  private ApplicationContext _db;

  public CinemaSessionService(ApplicationContext db)
  {
    _db = db;
  }

  public bool CreateSession(DateTime showingTime,
                            Film film,
                            CinemaHall hall,
                            User user)
  {
    if (showingTime < DateTime.Now)
      return false;
    
    DateTime finishShow = showingTime;
    finishShow.AddMinutes(film.Duration);
    CinemaSession? sessionNowInThisHall = _db.CinemaSession.Where(
      s => s.ShowingTime < finishShow &&
           s.ShowingTime.AddMinutes(s.ShowingFilm.Duration) > showingTime &&
           s.CinemaHallID == hall.ID
    ).FirstOrDefault();

    if (sessionNowInThisHall != null)
      return false;

    // One user can lead more than one session in moment, no problem
    CinemaSession sesion = new CinemaSession(user, film, hall, showingTime);
    _db.CinemaSession.Add(sesion);
    _db.SaveChanges();

   string action = $"Created session {sesion.ID} showing time: {showingTime.ToUniversalTime()}, film: {film.Name}, user: {user.LastName + " " + user.FirstName + " " + user.Surname}, cinema hall: {hall.Name}";

    _db.Log.Add(new Log(user, action));
    return true;
  }

  public int SellTicket(int sessionID, int sitNumber)
  {
    int default_ret = -1;

    CinemaSession ?session = _db.CinemaSession.Where(
        s => s.ID == sessionID).FirstOrDefault();

    if (session == null || DateTime.Now > session.ShowingTime)
      return default_ret;
     
    if (session.Hall.Capacity <= _db.Ticket.Count(
        t => t.SessionID == session.ID)
       )
      return default_ret;

    if (_db.Ticket.Any(t => t.SessionID == session.ID && 
                       t.SitNumber == sitNumber))
      return default_ret;
    
    Ticket ticket = new Ticket(session.ID, session.UserID, sitNumber);
    _db.Ticket.Add(ticket);
    _db.SaveChanges();
    User user = _db.User.Where(u => u.ID == session.UserID).Single();
    string film = _db.Film.Where(f => f.ID == session.FilmID).Select(f => f.Name).Single();
    string action = $"Sell ticket {ticket.ID}, user: {user.LastName + " " + user.FirstName + " " + user.Surname}, session: {session.ID}, film: {film}";
    _db.Log.Add(new Log(user, action));
    _db.SaveChanges();
    return ticket.ID; // TODO: need verify that
  }
}
