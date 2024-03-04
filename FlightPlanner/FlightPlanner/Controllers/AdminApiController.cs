using FlightPlanner.Models;
using FlightPlanner.Storage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers;

[Authorize]
[Route("admin-api")]
[ApiController]
public class AdminApiController : ControllerBase
{
   private static readonly object _lock = new();
   
   [HttpGet]
   [Route("flights/{id}")]
   public IActionResult GetFlight(int id)
   {
      var flight = FlightStorage.GetFlightById(id);
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
         if (FlightStorage.IsDuplicate(flight))
            return Conflict();
      

         if (flight.From.City.ToUpper().Trim() == flight.To.City.ToUpper().Trim() &&
             flight.From.Country.ToUpper().Trim() == flight.To.Country.ToUpper().Trim() &&
             flight.From.AirportCode.ToUpper().Trim() == flight.To.AirportCode.ToUpper().Trim())
         {
            return BadRequest();
         }
      
         if (string.IsNullOrEmpty(flight.From.City) ||
             string.IsNullOrEmpty(flight.From.Country) ||
             string.IsNullOrEmpty(flight.From.AirportCode) ||
             string.IsNullOrEmpty(flight.To.City) ||
             string.IsNullOrEmpty(flight.To.Country) ||
             string.IsNullOrEmpty(flight.To.AirportCode) ||
             string.IsNullOrEmpty(flight.Carrier) ||
             string.IsNullOrEmpty(flight.DepartureTime) ||
             string.IsNullOrEmpty(flight.ArrivalTime)
            )
         {
            return BadRequest();
         }
      
         if (DateTime.Parse(flight.DepartureTime) >= DateTime.Parse(flight.ArrivalTime) ||
             DateTime.Parse(flight.DepartureTime) < DateTime.Parse(flight.ArrivalTime).AddDays(-90))
         {
            return BadRequest();
         }
      
         FlightStorage.AddFlight(flight);
      
         return Created("", flight);
      }
   }

   [HttpDelete]
   [Route("flights/{id}")]
   public IActionResult DeleteFlight(int id)
   {
      lock (_lock)
      {
         FlightStorage.DeleteFlight(id);

         return Ok();
      }
   }
}  
