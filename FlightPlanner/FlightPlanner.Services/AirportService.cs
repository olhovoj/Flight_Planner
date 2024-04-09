
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Data;

namespace FlightPlanner.Services;

public class AirportService : EntityService<Airport>, IAirportService
{
    public AirportService(IFlightPlannerDbContext context) : base(context)
    {
    }

    public List<Airport> SearchAirportsByPhrase(string phrase)
    {
        phrase = phrase.ToUpper().Trim();
        
        var matchingAirports = _context.Airports
            .Where(a =>
                a.AirportCode.ToUpper().Contains(phrase) ||
                a.Country.ToUpper().Contains(phrase) ||
                a.City.ToUpper().Contains(phrase))
            .Distinct()
            .ToList();
        
        return matchingAirports;
    }
}