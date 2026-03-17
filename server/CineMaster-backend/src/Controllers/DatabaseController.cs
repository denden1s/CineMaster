using CineMaster_backend.src.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CineMaster_backend.src.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class DatabaseController : ControllerBase
  {
    private readonly ApplicationContext _db;
    private readonly DatabaseService _dbService;

    public DatabaseController(ApplicationContext db)
    {
      _db = db;
      _dbService = new DatabaseService(_db);
    }

    [HttpPost]
    [Route("generate")]
    public IActionResult Generate([FromBody] string password)
    {
      try
      {
        bool isGenerated = _dbService.Generate(password);
        if (!isGenerated)
          throw new Exception("Db not generated");
        return Ok();
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }
  }
}
