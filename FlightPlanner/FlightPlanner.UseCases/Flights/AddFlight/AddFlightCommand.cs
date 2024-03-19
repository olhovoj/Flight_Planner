using FlightPlanner.UseCases.Models;
using MediatR;

namespace FlightPlanner.UseCases.Flights.AddFlight;

public class AddFlightCommand(AddFlightRequest addFlightRequest) : IRequest<ServiceResult>
{
    public AddFlightRequest AddFlightRequest { get; set; } = addFlightRequest;
}