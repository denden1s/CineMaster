using System;

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

  public User(User user)
  {
    // var properties = GetType().GetProperties()
    //         .Where(p => p.CanWrite); // только свойства, которые можно записать
        
    //     foreach (var prop in properties)
    //     {
    //         var value = prop.GetValue(user);
    //         prop.SetValue(this, value);
    //     }
    Login = user.Login;
    Password = user.Password;
    FirstName = user.FirstName;
    LastName = user.LastName;
    Surname = user.Surname;
    Role = user.Role  ;
  } 
}
