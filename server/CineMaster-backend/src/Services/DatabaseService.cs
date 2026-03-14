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

  public DatabaseService(ApplicationContext db)
  {
    _db = db;
  }

  public void Generate()
  {
    GenerateUsers();
  }
}
