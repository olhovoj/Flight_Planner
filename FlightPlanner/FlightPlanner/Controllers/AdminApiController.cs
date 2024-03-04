using FlightPlanner.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers;

[Authorize]
[Route("admin-api")]
[ApiController]
public class AdminApiController : ControllerBase
{
   private readonly FlightPlannerDbContext _context;
   private static readonly object _lock = new object();
   
   public AdminApiController(FlightPlannerDbContext context)
   {
      _context = context;
   }
   
   [HttpGet]
   [Route("flights/{id}")]
   public IActionResult GetFlight(int id)
   {
      var flight = _context.GetFlightById(id);
      
      if (flight == null)
      {
         return NotFound();
      }
   
      return Ok(flight);
   }
   
   [HttpPut]
   [Route("flights")]
   public IActionResult AddFlight(Flight flight)
   {
      lock (_lock)
      {
         if (_context.IsDuplicate(flight))
            return Conflict();
      
         if (ArgumentValidation.IsSameFromToAirport(flight) || 
             ArgumentValidation.IsAnyNull(flight) || 
             ArgumentValidation.IsDateInvalid(flight))
         {
            return BadRequest();
         }
      
         _context.AddFlight(flight);
      
         return Created("", flight);
      }
   }

   [HttpDelete]
   [Route("flights/{id}")]
   public IActionResult DeleteFlight(int id)
   {
      _context.DeleteFlight(id);
      
      return Ok();
   }
}  
