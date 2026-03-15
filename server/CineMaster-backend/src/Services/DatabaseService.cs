using CineMaster_backend.src.Entities;

namespace CineMaster_backend.src.Services;

// TODO: for generate data
public class DatabaseService
{
  private ApplicationContext _db; // TODO: test when 2 entities use ApplicationContext how sync data between it 

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
    _db.AddRange(users);
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
    _db.AddRange(dbGenres);
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

  public DatabaseService(ApplicationContext db)
  {
    _db = db;
  }

  public void Generate()
  {
    GenerateUsers();
    GenerateGenres();
    GenerateCinemaHalls();
  }
}
