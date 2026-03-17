using CineMaster_backend.src.DTO;
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
      List<CinemaSessionDto> sessions = _sessionService.Get();
      return Ok(sessions);
    }

    // GET: api/sessions/{id}/seats
    [HttpGet("{id}/seats")]
    public IActionResult GetSeats(int id)
    {
      var seats = _sessionService.GetSessionSeatInfo(id);
      if (seats == null)
        return NotFound();

      return Ok(seats);
    }

    [HttpPost]
    [Route("ticket")]
    public IActionResult SellTicket([FromBody] TicketDto ticket)
    {
      try
      {
        int isSold = _sessionService.SellTicket(ticket);
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
