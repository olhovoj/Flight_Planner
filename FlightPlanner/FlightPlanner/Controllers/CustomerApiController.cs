using FlightPlanner.Interfaces;
using FlightPlanner.Models;
using FlightPlanner.Storage;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FlightPlanner.Controllers;

[Route("api")]
[ApiController]
public class CustomerApiController : ControllerBase
{
    [HttpGet]
    [Route("airports")]
    public IActionResult SearchAirport(string search)
    {
        return Ok(FlightStorage.SearchAirportsByPhrase(search));
    }

    [HttpPost]
    [Route("flights/search")]
    public IActionResult SearchFlights([FromBody] SearchFlightsRequest request)
    {
        var searchResult = FlightStorage.SearchFlights(request);
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
        return FlightStorage.SearchFlightById(id) != null ? Ok(FlightStorage.SearchFlightById(id)) : NotFound();
    }
}