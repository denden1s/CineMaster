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
  }
}
