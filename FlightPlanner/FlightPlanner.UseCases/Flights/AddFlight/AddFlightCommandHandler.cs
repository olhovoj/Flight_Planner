using System.Net;
using AutoMapper;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.UseCases.Models;
using FluentValidation;
using MediatR;

namespace FlightPlanner.UseCases.Flights.AddFlight;

public class AddFlightCommandHandler : IRequestHandler<AddFlightCommand, ServiceResult>
{
    private readonly IFlightService _flightService;
    private readonly IMapper _mapper;
    private readonly IValidator<AddFlightRequest> _validator;

    public AddFlightCommandHandler(IFlightService flightService, IMapper mapper, IValidator<AddFlightRequest> validator)
    {
        _flightService = flightService;
        _mapper = mapper;
        _validator = validator;
    }
    
    public async Task<ServiceResult> Handle(AddFlightCommand request, CancellationToken cancellationToken)
    {
        var flight = _mapper.Map<Flight>(request.AddFlightRequest);
        var validationResult = await _validator.ValidateAsync(request.AddFlightRequest, cancellationToken);
        var response = new ServiceResult();
        if (!validationResult.IsValid)
        {
            response.Status = HttpStatusCode.BadRequest;
            response.ResultObject = validationResult.Errors;
            return response;
        }
        
        if (_flightService.IsDuplicateFlight(flight))
        {
            response.Status = HttpStatusCode.Conflict;
            response.ResultObject = new string("Duplicate flight");
            
            return response;
        }
        
        _flightService.Create(flight);
        
        response.Status = HttpStatusCode.Created;
        response.ResultObject = _mapper.Map<FlightViewModel>(flight);

        return response;
    }
}