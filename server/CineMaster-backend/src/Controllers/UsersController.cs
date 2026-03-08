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
    public IActionResult Create([FromBody] CreateUserDto createUserDto)
    {
      try
      {
        bool isCreated = _userService.Create(createUserDto);
        // TODO:
        // 201 Created + ссылка на GET + сам объект
        //return CreatedAtAction(nameof(GetById), new { id = user?.ID }, true);

        return Ok(isCreated);
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }
  }
}
