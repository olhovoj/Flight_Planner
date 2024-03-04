using FlightPlanner.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightPlanner;

public class FlightPlannerDbContext : DbContext
{
    public FlightPlannerDbContext(DbContextOptions<FlightPlannerDbContext> options) : 
        base(options)
    {
        
    }
    
    public DbSet<Flight> Flights { get; set; }
    public DbSet<Airport> Airports { get; set; }
    
    public void AddFlight(Flight flight)
    {
        Flights.Add(flight);
        SaveChanges();
    }
    
    public bool IsDuplicate(Flight flight)
    {
        return Flights
            .Include(airport => airport.From)
            .Include(airport => airport.To)
            .OrderBy(storedFlight => storedFlight.Id)
            .Any(storedFlight =>
                storedFlight.From.City == flight.From.City &&
                storedFlight.From.Country == flight.From.Country &&
                storedFlight.From.AirportCode == flight.From.AirportCode &&
                storedFlight.To.City == flight.To.City &&
                storedFlight.To.Country == flight.To.Country &&
                storedFlight.To.AirportCode == flight.To.AirportCode &&
                storedFlight.Carrier == flight.Carrier &&
                storedFlight.DepartureTime == flight.DepartureTime &&
                storedFlight.ArrivalTime == flight.ArrivalTime);
    }

    public void Clear()
    {
        Database.ExecuteSqlRaw("DBCC CHECKIDENT ('Flights', RESEED, 0)");
        Database.ExecuteSqlRaw("DBCC CHECKIDENT ('Airports', RESEED, 0)");
        Flights.RemoveRange(Flights);
        Airports.RemoveRange(Airports);
        SaveChanges();
    }
    
    public Flight? GetFlightById(int id)
    {
        return Flights
            .Include(airport => airport.From)
            .Include(airport => airport.To)
            .FirstOrDefault(flight => flight.Id == id);
    }
    
    public void DeleteFlight(int id)
    {
        var flight = GetFlightById(id);
        if (flight == null)
        {
            return;
        }
        
        Flights.Remove(flight);
        SaveChanges();
    }
    
    public List<Airport> SearchAirportsByPhrase(string phrase)
    {
        phrase = phrase.ToUpper().Trim();
        
        var matchingAirports = Airports
            .Where(a =>
                a.AirportCode.ToUpper().Contains(phrase) ||
                a.Country.ToUpper().Contains(phrase) ||
                a.City.ToUpper().Contains(phrase))
            .Distinct()
            .ToList();

        return matchingAirports;
    }
    
    public List<Flight>? SearchFlights(SearchFlightsRequest request)
    {
        
        if (string.IsNullOrEmpty(request.From) && 
            string.IsNullOrEmpty(request.To) &&
            string.IsNullOrEmpty(request.DepartureDate))
        {
            return null;
        }
        
        var results = Flights
            .Include(airport => airport.From)
            .Include(airport => airport.To)
            .Where(flight =>
                (flight.From.AirportCode == request.From) ||
                (flight.To.AirportCode == request.To) ||
                (flight.DepartureTime == request.DepartureDate))
            .ToList();

        return results;
    }
}