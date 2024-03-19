
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
    
    // public void DeleteUnusedAirports()
    // {
    //     var unusedAirports = _context.Airports
    //         .Where(airport => !_context.Flights
    //             .Any(flight => 
    //                 flight.From.Id == airport.Id || 
    //                 flight.To.Id == airport.Id))
    //         .ToList();
    //
    //     _context.Airports.RemoveRange(unusedAirports);
    //     _context.SaveChanges();
    // }
}