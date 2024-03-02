using FlightPlanner.Models;

namespace FlightPlanner.Handlers;

public class ArgumentValidation
{
    public static bool IsSameFromToAirport(Flight flight)
    {
        return flight.From.City.ToUpper().Trim() == flight.To.City.ToUpper().Trim() &&
               flight.From.Country.ToUpper().Trim() == flight.To.Country.ToUpper().Trim() &&
               flight.From.AirportCode.ToUpper().Trim() == flight.To.AirportCode.ToUpper().Trim();
    }
    
    public static bool IsAnyNull(Flight flight)
    {
        var propertiesToCheck = new[]
        {
            flight.From.City, flight.From.Country, flight.From.AirportCode,
            flight.To.City, flight.To.Country, flight.To.AirportCode,
            flight.Carrier, flight.DepartureTime, flight.ArrivalTime
        };

        return propertiesToCheck.Any(string.IsNullOrEmpty);
    }
    
    public static bool IsDateInvalid(Flight flight)
    {
        return DateTime.Parse(flight.DepartureTime) >= DateTime.Parse(flight.ArrivalTime) ||
               DateTime.Parse(flight.DepartureTime) < DateTime.Parse(flight.ArrivalTime).AddDays(-90);
    }
}

