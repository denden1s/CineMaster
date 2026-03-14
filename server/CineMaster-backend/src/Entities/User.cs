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
    Encryption encryption = new Encryption();
    Login = user.Login;
    Password = encryption.Hash(user.Password);
    FirstName = user.FirstName;
    LastName = user.LastName;
    Surname = user.Surname;
    Role = user.Role;
  }
}
