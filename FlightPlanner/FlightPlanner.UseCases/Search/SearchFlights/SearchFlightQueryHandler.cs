using System.Net;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.UseCases.Models;
using FluentValidation;
using MediatR;

namespace FlightPlanner.UseCases.Search.SearchFlights;

public class SearchFlightQueryHandler : IRequestHandler<SearchFlightQuery, ServiceResult>
{
    private readonly IFlightService _flightService;
    private readonly IValidator<SearchFlightsRequest> _validator;
    

    public SearchFlightQueryHandler(IFlightService flightService, IValidator<SearchFlightsRequest> validator)
    {
        _flightService = flightService;
        _validator = validator;
    }

    public async Task<ServiceResult> Handle(SearchFlightQuery request, CancellationToken cancellationToken)
    {
        var validatorResult = _validator.Validate(request.SearchFlightsRequest);
        if (!validatorResult.IsValid) 
            return new ServiceResult
            {
                Status = HttpStatusCode.BadRequest,
                ResultObject = validatorResult.Errors
            };
        
        var searchResult = _flightService.SearchFlights(request.SearchFlightsRequest);
        if (searchResult.Items == null)
        {
            return new ServiceResult
            {
                Status = HttpStatusCode.BadRequest,
                ResultObject = new string("Flight does not exist.")
            };
        }
        
        return new ServiceResult
        {
            Status = HttpStatusCode.OK,
            ResultObject = searchResult
        };
    }
}