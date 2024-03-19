using System.Net;
using FlightPlanner.Core.Services;
using FlightPlanner.UseCases.Models;
using MediatR;

namespace FlightPlanner.UseCases.Flights.DeleteFlightCommand;

public class DeleteFlightCommandHandler : IRequestHandler<DeleteFlightCommand, ServiceResult>
{
    private readonly IFlightService _flightService;

    public DeleteFlightCommandHandler(IFlightService flightService)
    {
        _flightService = flightService;
    }

    public async Task<ServiceResult> Handle(DeleteFlightCommand request, CancellationToken cancellationToken)
    {
        _flightService.DeleteFlight(request.Id);
        return new ServiceResult
        {
            Status = HttpStatusCode.OK,
            ResultObject = new string("Deleted successfully")
        };
    }
}