namespace CineMaster_frontend.Models;

public enum UserRole
{
    kUser,
    kAdmin,
    kAnalyst,
}

public class UserDto
{
    public int ID { get; set; }
    public string Login { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Surname { get; set; }
    public UserRole Role { get; set; }
}
