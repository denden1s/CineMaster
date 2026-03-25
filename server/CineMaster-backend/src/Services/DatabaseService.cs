using CineMaster_backend.src.Entities;

namespace CineMaster_backend.src.Services;

public class DatabaseService
{
  private ApplicationContext _db;
  private const string kPassword = "CineMaster";
  private void GenerateUsers()
  {
    List<string> names =
    [
      "Александр",
      "Дмитрий",
      "Иван",
      "Илья",
      "Николай",
      "Павел",
    ];
    List<string> lastNames =
    [
      "Круглов",
      "Смирнов",
      "Иванов",
      "Курочкин",
      "Сидоров",
      "Петров",
    ];
    List<string> surnames =
    [
      "Александрович",
      "Дмитриевич",
      "Иванович",
      "Ильич",
      "Николаевич",
      "Павлович",
    ];

    string login_template = "user_";
    string password_template = "pwd_";
    int user_counter = 1;
    List<User> users = new List<User>();
    Random rnd = new Random();
    foreach(var name in names)
      foreach(var lastName in lastNames)
        foreach(var surname in surnames)
        {
          users.Add(new User(
            login_template + Convert.ToString(user_counter),
            password_template + Convert.ToString(user_counter++),
            name,
            lastName,
            surname,
            (UserRole)rnd.Next((int)UserRole.kUser, (int)UserRole.kAnalyst + 1)
          ));
        }
    int userRoleCount = users.Count(u => u.Role == UserRole.kUser);
    int adminRoleCount = users.Count(u => u.Role == UserRole.kAdmin);
    int analystRoleCount = users.Count(u => u.Role == UserRole.kAnalyst);
    _db.User.AddRange(users);
    _db.SaveChanges();
  }

  private void GenerateGenres()
  {
    List<string> genres =
    [
      "биографический",
      "боевик",
      "вестерн",
      "военный",
      "детектив",
      "детский",
      "документальный",
      "драма",
      "исторический",
      "кинокомикс",
      "комедия",
      "короткометражный",
      "криминал",
      "мелодрама",
      "мистика",
      "мультфильм",
      "мюзикл",
      "научный",
      "нуар",
      "приключения",
      "семейный",
      "спорт",
      "триллер",
      "ужасы",
      "фантастика",
      "фэнтези",
    ];
    List<Genre> dbGenres = new List<Genre>();
    foreach(var genre in genres)
    {
      dbGenres.Add(new Genre(genre));
    }
    _db.Genre.AddRange(dbGenres);
    _db.SaveChanges();
  }

  private void GenerateCinemaHalls()
  {
    string baseHallTemplate = "Кинозал №";
    string vipHallTemplate = "Кинозал повышенного комфорта №";
    string superVipHallTemplate = "VIP кинозал №";
    Random rnd = new Random();
    int baseHallCount = rnd.Next(1, 10);
    int vipHallCount = rnd.Next(1, 10);
    int superVipHallCount = rnd.Next(1, 10);

    List<CinemaHall> halls = new List<CinemaHall>();
    for(int i = 1; i < baseHallCount + 1; i++)
    {
      halls.Add(new CinemaHall(
        baseHallTemplate + Convert.ToString(i),
        rnd.Next(30, 100),
        rnd.Next(10, 100)
      ));
    }

    for(int i = 1; i < vipHallCount + 1; i++)
    {
       halls.Add(new CinemaHall(
        vipHallTemplate + Convert.ToString(i),
        rnd.Next(20, 50),
        rnd.Next(30, 60)
      ));
    }

    for(int i = 1; i < superVipHallCount + 1; i++)
    {
       halls.Add(new CinemaHall(
        superVipHallTemplate + Convert.ToString(i),
        rnd.Next(10, 30),
        rnd.Next(50, 100)
      ));
    }
    _db.CinemaHall.AddRange(halls);
    _db.SaveChanges();
  }

  private void GenerateFilms()
  {
    List<Genre> genres = _db.Genre.ToList();
    if (genres.Count() == 0)
      return;

    List<Film> films = new List<Film>();
    films.Add(new Film(
      genres.Single(g => g.Name.Equals("фантастика")).ID,
      "Интерстеллар",
      "https://avatars.mds.yandex.net/get-kinopoisk-image/1773646/635d7f90-a68b-4010-8825-3eb972c897ac/3840x",
      169,
      15
    ));
    
    films.Add(new Film(
      genres.Single(g => g.Name.Equals("драма")).ID,
      "Побег из Шоушенка",
      "https://avatars.mds.yandex.net/get-kinopoisk-image/1900788/2fbb9b8d-4f86-45fb-b42c-8c7b0e7ac7a5/3840x",
      142,
      13
    ));

    films.Add(new Film(
      genres.Single(g => g.Name.Equals("драма")).ID,
      "1+1",
      "https://avatars.mds.yandex.net/get-kinopoisk-image/1898899/4c78b2e3-ffed-4985-bbc2-642952c92f5e/orig",
      112,
      10
    ));

    films.Add(new Film(
      genres.Single(g => g.Name.Equals("криминал")).ID,
      "Джентльмены",
      "https://avatars.mds.yandex.net/get-kinopoisk-image/1600647/b0818ad5-daeb-420b-a37a-66186d209788/3840x",
      113,
      12
    ));

    films.Add(new Film(
      genres.Single(g => g.Name.Equals("триллер")).ID,
      "Остров проклятых",
      "https://avatars.mds.yandex.net/get-kinopoisk-image/4303601/5a65e52e-05e4-48db-a8d8-1c7da810c38c/3840x",
      138,
      15
    ));

    films.Add(new Film(
      genres.Single(g => g.Name.Equals("драма")).ID,
      "Зеленая миля",
      "https://avatars.mds.yandex.net/get-kinopoisk-image/1946459/acb932eb-c7d0-42de-92df-f5f306c4c48e/3840x",
      189,
      17
    ));

    films.Add(new Film(
      genres.Single(g => g.Name.Equals("фантастика")).ID,
      "Терминатор 2: Судный день",
      "https://avatars.mds.yandex.net/get-kinopoisk-image/10893610/2dd14742-f241-42ca-9db4-331e3a483c50/3840x",
      137,
      11
    ));

    _db.Film.AddRange(films);
    _db.SaveChanges();
  }

  private void GenerateCinemaSessions()
  {
    if(_db.Film.Count() * _db.User.Count() * _db.CinemaHall.Count() == 0)
      return;
    
    List<CinemaSession> sessions = new List<CinemaSession>();
    List<int> usersID = _db.User.Where(u => u.Role == UserRole.kUser)
                                .Select(u => u.ID).ToList();

    List<int> hallsID = _db.CinemaHall.Select(h => h.ID).ToList();

    List<int> filmsID = _db.Film.Select(f => f.ID).ToList();
    Random rnd = new Random();
    int dayCounter = 1;
    DateTime date = DateTime.Today.AddDays(dayCounter++).AddHours(10);
    int userID = 0;
    int hallID = 0;
    foreach(var film in filmsID)
    {
      userID = rnd.Next(0, usersID.Count());
      hallID = rnd.Next(0, hallsID.Count());
      sessions.Add(new CinemaSession(
        usersID[userID], 
        film,
        hallsID[hallID],
        date
        ));
        if (date.Hour >= 20)
          date = DateTime.Today.AddDays(dayCounter++).AddHours(10);
        else
        {
          int filmDuration = _db.Film.Where(f => f.ID == film)
                                     .Select(f => f.Duration).First();

          date = date.AddMinutes(filmDuration + 30);
        }
    }

    _db.CinemaSession.AddRange(sessions);
    _db.SaveChanges();
  }

  private void SellTickets()
  {
    List<Ticket> tickets = new List<Ticket>();
    List<CinemaSession> sessions = _db.CinemaSession.ToList();
    Random rnd = new Random();
    int hallCapacity = 0;
    foreach (var session in sessions)
    {
      hallCapacity = _db.CinemaHall.Where(h => h.ID == session.CinemaHallID)
                                   .Select(h => h.Capacity).Single();
      int ticketsToSell = rnd.Next(1, hallCapacity);
      for (int i = 1; i <= ticketsToSell; i++)
      {
        tickets.Add(new Ticket(session, i));
      }
    }
    _db.Ticket.AddRange(tickets);
    _db.SaveChanges();
  }


  private void Clear()
  {
    _db.User.RemoveRange(_db.User);
    _db.Genre.RemoveRange(_db.Genre);
    _db.Ticket.RemoveRange(_db.Ticket);
    _db.CinemaSession.RemoveRange(_db.CinemaSession);
    _db.CinemaHall.RemoveRange(_db.CinemaHall);
    _db.Film.RemoveRange(_db.Film);
    _db.Log.RemoveRange(_db.Log);
    _db.SaveChanges();
  }
  public DatabaseService(ApplicationContext db)
  {
    _db = db;
  }

  public bool Generate(string password)
  {
    if (!kPassword.Equals(password))
      return false;

    Clear();
    GenerateUsers();
    GenerateGenres();
    GenerateCinemaHalls();
    GenerateFilms();
    GenerateCinemaSessions();
    SellTickets();
    return true;
  }
}
