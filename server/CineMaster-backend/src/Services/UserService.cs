using CineMaster_backend.src.DTO;
using CineMaster_backend.src.Entities;

namespace CineMaster_backend.src.Services;

public class UserService
{
  private ApplicationContext _db;

  private bool isExist(User user)
  {
    return _db.User.Any(u => u.Login == user.Login);
  }

  private User? GetUser(User user)
  {
    return _db.User.Where(u => u.Login == user.Login &&
                          u.Password == user.Password).FirstOrDefault();
  }

  public UserService(ApplicationContext db)
  {
    _db = db;
  }
  public bool Create(CreateUserDto dto)
  {
    User user = new User(dto);
    if (isExist(user))
      return false;
    _db.User.Add(user);
    return _db.SaveChanges() == 1;
  }

  public UserDto? Get(AuthUserDto authUser)
  {
    User user = new User(authUser);

    if (!isExist(user))
      return null;
    
    User ?db_user = GetUser(user);
    if (db_user == null)
      return null;

    return new UserDto(db_user);
  }
}
