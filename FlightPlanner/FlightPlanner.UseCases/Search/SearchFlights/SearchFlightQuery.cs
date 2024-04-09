using FlightPlanner.Core.Models;
using FlightPlanner.UseCases.Models;
using MediatR;

namespace FlightPlanner.UseCases.Search.SearchFlights;

public class SearchFlightQuery(SearchFlightsRequest searchFlightsRequest) : IRequest<ServiceResult>
{
    public SearchFlightsRequest SearchFlightsRequest { get; } = searchFlightsRequest;
}