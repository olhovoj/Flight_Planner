using FlightPlanner.Core.Models;

namespace FlightPlanner.Core.Services;

public interface IFlightService : IEntityService<Flight>
{
    Flight? GetFullFlightById(int id);
    bool IsDuplicateFlight(Flight flight);
    void DeleteFlight(int id);
    PageResult<Flight> SearchFlights(SearchFlightsRequest request);
}