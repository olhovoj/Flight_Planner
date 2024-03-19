using FlightPlanner.Core.Models;

namespace FlightPlanner.Core.Services;

public interface IAirportService : IEntityService<Airport>
{
    List<Airport> SearchAirportsByPhrase(string phrase);
    // void DeleteUnusedAirports();
}