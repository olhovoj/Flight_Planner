using System.Net;
using AutoMapper;
using FlightPlanner.Core.Services;
using FlightPlanner.Services;
using FlightPlanner.UseCases.Models;
using MediatR;

namespace FlightPlanner.UseCases.Search.SearchAirport;

public class SearchAirportQueryHandler : IRequestHandler<SearchAirportQuery, ServiceResult>
{
    private readonly IAirportService _airportService;
    private readonly IMapper _mapper;

    public SearchAirportQueryHandler(
        IAirportService airportService,
        IMapper mapper)
    {
        _airportService = airportService;
        _mapper = mapper;
    }

    public async Task<ServiceResult> Handle(SearchAirportQuery request, CancellationToken cancellationToken)
    {
        var airportViews = _mapper.Map<List<AirportViewModel>>(_airportService.SearchAirportsByPhrase(request.Search));
        if (airportViews == null)
        {
            return new ServiceResult
            {
                Status = HttpStatusCode.NotFound,
                ResultObject = new string("Airport does not exist.")
            };
        }
        
        return new ServiceResult
        {
            Status = HttpStatusCode.OK,
            ResultObject = airportViews
        };
    }
}