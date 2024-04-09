using FlightPlanner.UseCases.Models;
using MediatR;

namespace FlightPlanner.UseCases.Search.SearchAirport;

public class SearchAirportQuery(string search) : IRequest<ServiceResult>
{
    public string Search { get; } = search;
}