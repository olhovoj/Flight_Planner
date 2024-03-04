using FlightPlanner.Interfaces;
using FlightPlanner.Models;

namespace FlightPlanner.Storage;

public class FlightStorage
{
    private static readonly List<Flight> _flights = new();
    private static int _id = 1;

    public static void AddFlight(Flight flight)
    {
        flight.Id = _id++;
        _flights.Add(flight);
    }

    public static bool IsDuplicate(Flight flight)   
    {
        return _flights.LastOrDefault(storedFlight =>
            storedFlight.From.City == flight.From.City &&
            storedFlight.From.Country == flight.From.Country &&
            storedFlight.From.AirportCode == flight.From.AirportCode &&
            storedFlight.To.City == flight.To.City &&
            storedFlight.To.Country == flight.To.Country &&
            storedFlight.To.AirportCode == flight.To.AirportCode &&
            storedFlight.Carrier == flight.Carrier &&
            storedFlight.DepartureTime == flight.DepartureTime &&
            storedFlight.ArrivalTime == flight.ArrivalTime) != null;
    }

    public static Flight? GetFlightById(int id)
    {
        return _flights.FirstOrDefault(flight => flight.Id == id);
    }

    public static void DeleteFlight(int id)
    {
        _flights.RemoveAll(flight => flight.Id == id);
    }

    public static List<Airport> SearchAirportsByPhrase(string phrase)
    {
        phrase = phrase.ToUpper().Trim();
        
        var matchingAirports = _flights.SelectMany(f => new[] { f.From, f.To })
            .Where(a =>
                a.AirportCode.ToUpper().Contains(phrase) ||
                a.Country.ToUpper().Contains(phrase) ||
                a.City.ToUpper().Contains(phrase))
            .Distinct()
            .ToList();

        return matchingAirports;
    }

    public static List<Flight>? SearchFlights(SearchFlightsRequest request)
    {
        
        if (string.IsNullOrEmpty(request.From) && 
            string.IsNullOrEmpty(request.To) &&
            string.IsNullOrEmpty(request.DepartureDate))
        {
            return null;
        }
        
        var results = _flights.Where(flight =>
            (flight.From.AirportCode == request.From) ||
            (flight.To.AirportCode == request.To) ||
            (flight.DepartureTime == request.DepartureDate)
        ).ToList();

        return results;
    }

    public static Flight? SearchFlightById(int id)
    {
        return _flights.FirstOrDefault(flight => flight.Id == id);
    }
    
    public static void Clear()
    {
        _flights.Clear();
    }
}