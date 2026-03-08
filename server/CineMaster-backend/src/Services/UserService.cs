using CineMaster_backend.src.DTO;
using CineMaster_backend.src.Entities;

namespace CineMaster_backend.src.Services;

public class UserService
{
  private ApplicationContext _db;

  public UserService(ApplicationContext db)
  {
    _db = db;
  }
  public bool Create(CreateUserDto dto)
  {
    User user = new User(dto);
    if (_db.User.Any(u => u.Login == user.Login))
      return false;
    _db.User.Add(user);
    return _db.SaveChanges() == 1;
  }
}
