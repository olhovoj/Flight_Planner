using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers;

[Route("testing-api")]
[ApiController]
public class CleanupApiController : ControllerBase
{
    private FlightPlannerDbContext _context;
    
    public CleanupApiController(FlightPlannerDbContext context)
    {
        _context = context;
    }
    
    [HttpPost]
    [Route("clear")]
    public IActionResult Clear()
    {
        _context.Clear();
        
        return Ok();
    }
}