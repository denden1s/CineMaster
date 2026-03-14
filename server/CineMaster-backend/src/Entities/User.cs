using CineMaster_backend.src.DTO;
using CineMaster_backend.src.Utils;

namespace CineMaster_backend.src.Entities;

public enum UserRole
{
  kUser,
  kAdmin,
  kAnalyst,
}

public class User
{
  private string SetPassword(string password, string salt)
  {
    Encryption encryption = new Encryption();
    string data = password + salt; 
    return encryption.Hash(data);
  }

  public int ID { get; private set; }
  public string Login { get; private set; }
  public string Password { get; private set; }
  public string FirstName { get; private set; }
  public string LastName { get; private set; }
  public string Surname { get; private set; }
  public UserRole Role { get; private set; }
  public List<Log> Logs { get; private set; }
  public List<Ticket> Tickets { get; private set; }
  public List<CinemaSession> Sessions { get; private set; }
  
  public User() { }
  public User(CreateUserDto user)
  {
    Login = user.Login;
    Password = SetPassword(user.Password, Login);
    FirstName = user.FirstName;
    LastName = user.LastName;
    Surname = user.Surname;
    Role = user.Role;
  }

  public User(AuthUserDto user)
  {
    Login = user.Login;
    Password = SetPassword(user.Password, Login);
  }
}
