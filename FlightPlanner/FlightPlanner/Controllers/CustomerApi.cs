using FlightPlanner.Interfaces;
using FlightPlanner.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers;

[Route("api")]
[ApiController]
public class CustomerApi : ControllerBase
{
    private readonly FlightPlannerDbContext _context;
   
    public CustomerApi(FlightPlannerDbContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    [Route("airports")]
    public IActionResult SearchAirport(string search)
    {
        return Ok(_context.SearchAirportsByPhrase(search));
    }

    [HttpPost]
    [Route("flights/search")]
    public IActionResult SearchFlights([FromBody] SearchFlightsRequest request)
    {
        var searchResult = _context.SearchFlights(request);
        if (searchResult == null || request.From == request.To)
        {
            return BadRequest();
        }
        
        var result = new PageResult<Flight>
        {
            Page = 0,
            TotalItems = searchResult.Count,
            Items = searchResult
        };
        
        return Ok(result);
    }
    
    [HttpGet]
    [Route("flights/{id}")]
    public IActionResult SearchFlightById(int id)
    {
        return _context.GetFlightById(id) != null ? Ok(_context.GetFlightById(id)) : NotFound();
    }
}