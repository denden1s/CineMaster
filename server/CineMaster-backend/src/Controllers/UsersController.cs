using CineMaster_backend.src.DTO;
using CineMaster_backend.src.Services;
using Microsoft.AspNetCore.Mvc;

namespace CineMaster_backend.src.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class UsersController : ControllerBase
  {
    private readonly ApplicationContext _db;
    private readonly UserService _userService;
    
    // DI контейнер вызывает этот конструктор
    public UsersController(ApplicationContext db)
    {
      _db = db;
      _userService = new UserService(_db);
    }

    // POST: api/users
    [HttpPost]
    [Route("api/users/create")]
    public IActionResult Create([FromBody] CreateUserDto createUserDto)
    {
      try
      {
        bool isCreated = _userService.Create(createUserDto);
        if (!isCreated)
          throw new Exception("User not created");
        return Ok();
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpPost]
    [Route("api/users/auth")]
    public IActionResult Auth([FromBody] AuthUserDto authUserDto)
    {
      try
      {
        UserDto ?user = _userService.Get(authUserDto);
        if (user == null)
          return NotFound();

        return Ok(user);
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }
  }
}
