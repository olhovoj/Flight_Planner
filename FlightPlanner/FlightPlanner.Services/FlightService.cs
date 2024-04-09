using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Data;
using FluentValidation.Validators;
using Microsoft.EntityFrameworkCore;

namespace FlightPlanner.Services;

public class FlightService : EntityService<Flight>, IFlightService
{
    public FlightService(IFlightPlannerDbContext context) : base(context)
    {
    }

    public Flight? GetFullFlightById(int id)
    {
        return _context.Flights
            .Include(airport => airport.From)
            .Include(airport => airport.To)
            .SingleOrDefault(flight => flight.Id == id);
       
    }
    
    public bool IsDuplicateFlight(Flight flight)
    {
        lock (_lock)
        {
            return _context.Flights
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
    }
    
    public void DeleteFlight(int id)
    {
        var flight = GetFullFlightById(id);
        if (flight == null)
        {
            return;
        }
        
        Delete(flight);
    }
    
    public PageResult<Flight> SearchFlights(SearchFlightsRequest request)
    {
        var results = _context.Flights
            .Include(airport => airport.From)
            .Include(airport => airport.To)
            .Where(flight =>
                (flight.From.AirportCode == request.From) ||
                (flight.To.AirportCode == request.To) ||
                (flight.DepartureTime == request.DepartureDate))
            .ToList();

        return new PageResult<Flight>
        {
            Page = 0,
            TotalItems = results.Count,
            Items = results
        };
    }
}