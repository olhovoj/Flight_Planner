using FlightPlanner.Extensions;
using FlightPlanner.UseCases.Flights.AddFlight;
using FlightPlanner.UseCases.Flights.DeleteFlightCommand;
using FlightPlanner.UseCases.Flights.GetFlight;
using FlightPlanner.UseCases.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers;

[Authorize]
[Route("admin-api/flights")]
[ApiController]
public class AdminApiController : ControllerBase
{
   private readonly IMediator _mediator;
   
   public AdminApiController(IMediator mediator)
   {
      _mediator = mediator;
   }
    
   [HttpGet]
   [Route("{id:int}")]
   public async Task<IActionResult> GetFlight(int id)
   {
      return (await _mediator
         .Send(new GetFlightQuery(id)))
         .ToActionResult();
   }
   
   [HttpPut]
   [Route("")]
   public async Task<IActionResult> AddFlight(AddFlightRequest request)
   {
      return (await _mediator
         .Send(new AddFlightCommand(request)))
         .ToActionResult();
   }

   [HttpDelete]
   [Route("{id:int}")]
   public async Task<IActionResult> DeleteFlight(int id)
   {
      return (await _mediator
         .Send(new DeleteFlightCommand(id)))
         .ToActionResult();
   }
}  