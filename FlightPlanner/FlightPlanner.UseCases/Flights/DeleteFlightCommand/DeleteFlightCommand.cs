using FlightPlanner.UseCases.Models;
using MediatR;

namespace FlightPlanner.UseCases.Flights.DeleteFlightCommand;

public class DeleteFlightCommand(int id) : IRequest<ServiceResult>
{
    public int Id { get; set; } = id;
}