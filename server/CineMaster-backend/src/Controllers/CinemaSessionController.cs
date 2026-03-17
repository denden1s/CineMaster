using CineMaster_backend.src.Entities;
using CineMaster_backend.src.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CineMaster_backend.src.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class CinemaSessionController : ControllerBase
  {
    private readonly ApplicationContext _db;
    private readonly CinemaSessionService _sessionService;

    public CinemaSessionController(ApplicationContext db)
    {
      _db = db;
      _sessionService = new CinemaSessionService(_db);
    }

    // GET: api/sessions
    [HttpGet]
    public IActionResult GetSessions()
    {
      List<CinemaSession> sessions = _sessionService.Get();
      return Ok(sessions);
    }

    [HttpPost]
    [Route("ticket")]
    public IActionResult SellTicket([FromBody] int sessionID, int sitNumber)
    {
      try
      {
        int isSold = _sessionService.SellTicket(sessionID, sitNumber);
        if (isSold == -1)
          throw new Exception("Ticket not created");
        return Ok(isSold);
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }
  }
}
