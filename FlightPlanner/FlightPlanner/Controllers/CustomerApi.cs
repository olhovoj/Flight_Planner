using FlightPlanner.Core.Models;
using FlightPlanner.Extensions;
using FlightPlanner.UseCases.Search.SearchAirport;
using FlightPlanner.UseCases.Search.SearchFlightById;
using FlightPlanner.UseCases.Search.SearchFlights;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers;

[Route("api")]
[ApiController]
public class CustomerApi : ControllerBase
{
    private readonly IMediator _mediator;
   
    public CustomerApi(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    [Route("airports")]
    public async Task<IActionResult> SearchAirport(string search)
    {
        return (await _mediator
            .Send(new SearchAirportQuery(search)))
            .ToActionResult();
    }

    [HttpPost]
    [Route("flights/search")]
    public async Task<IActionResult> SearchFlights([FromBody] SearchFlightsRequest request)
    {
        return (await _mediator
            .Send(new SearchFlightQuery(request)))
            .ToActionResult();
    }
    
    [HttpGet]
    [Route("flights/{id}")]
    public async Task<IActionResult> SearchFlightById(int id)
    {
        return (await _mediator
            .Send(new SearchFlightByIdQuery(id)))
            .ToActionResult();
    }
}