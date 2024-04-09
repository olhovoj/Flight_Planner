using System.Net;
using AutoMapper;
using FlightPlanner.Core.Services;
using FlightPlanner.UseCases.Models;
using MediatR;

namespace FlightPlanner.UseCases.Search.SearchFlightById;

public class SearchFlightByIdQueryHandler : IRequestHandler<SearchFlightByIdQuery, ServiceResult>
{
    private readonly IFlightService _flightService;
    private readonly IMapper _mapper;

    public SearchFlightByIdQueryHandler(IFlightService flightService, IMapper mapper)
    {
        _flightService = flightService;
        _mapper = mapper;
    }

    public async Task<ServiceResult> Handle(SearchFlightByIdQuery request, CancellationToken cancellationToken)
    {
        return _flightService.GetFullFlightById(request.Id) != null
            ? new ServiceResult
            {
                Status = HttpStatusCode.OK,
                ResultObject = _mapper.Map<FlightViewModel>(_flightService.GetFullFlightById(request.Id))
            }
            : new ServiceResult
            {
                Status = HttpStatusCode.NotFound,
                ResultObject = new string("Flight does not exist.")
            };
    }
}