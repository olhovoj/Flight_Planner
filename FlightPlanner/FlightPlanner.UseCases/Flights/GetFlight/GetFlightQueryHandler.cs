using System.Net;
using AutoMapper;
using FlightPlanner.Core.Services;
using FlightPlanner.UseCases.Models;
using MediatR;

namespace FlightPlanner.UseCases.Flights.GetFlight;

public class GetFlightQueryHandler : IRequestHandler<GetFlightQuery, ServiceResult>

{
    private readonly IFlightService _flightService;
    private readonly IMapper _mapper;
    
    public GetFlightQueryHandler(
        IFlightService flightService, 
        IMapper mapper)
    {
        _flightService = flightService;
        _mapper = mapper;
    }
    
    public async Task<ServiceResult> Handle(GetFlightQuery request, CancellationToken cancellationToken)
    {
        var flight = _flightService.GetFullFlightById(request.Id);
        var response = new ServiceResult();
        
        if (flight == null)
        {
            response.Status = HttpStatusCode.NotFound;
            return response;
        }
        
        response.ResultObject = _mapper.Map<FlightViewModel>(flight);
        response.Status = HttpStatusCode.OK;
        
        return response;
    }
}