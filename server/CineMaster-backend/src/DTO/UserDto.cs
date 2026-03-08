using CineMaster_backend.src.Entities;

namespace CineMaster_backend.src.DTO;

public class UserDto
{
  public int ID { get; set; }
  public string Login { get; set; }
  public string FirstName { get; set; }
  public string LastName { get; set; }
  public string Surname { get; set; }
  public UserRole Role { get; set; }
}

public class CreateUserDto
{
  public string Login { get; set; }
  public string Password { get; set; }
  public string FirstName { get; set; }
  public string LastName { get; set; }
  public string Surname { get; set; }
  public UserRole Role { get; set; } = UserRole.kUser;
}
