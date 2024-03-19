using FlightPlanner.UseCases.Models;
using MediatR;

namespace FlightPlanner.UseCases.Search.SearchFlightById;

public class SearchFlightByIdQuery(int id) : IRequest<ServiceResult>
{
    public int Id { get; } = id;
}