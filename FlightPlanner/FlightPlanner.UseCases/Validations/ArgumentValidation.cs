using FlightPlanner.UseCases.Models;

namespace FlightPlanner.UseCases.Validations;

public class ArgumentValidation
{
    public static bool IsDifferentFromToAirport(AddFlightRequest request)
    {
        return !(request.From.City.ToUpper().Trim() == request.To.City.ToUpper().Trim() &&
               request.From.Country.ToUpper().Trim() == request.To.Country.ToUpper().Trim() &&
               request.From.Airport.ToUpper().Trim() == request.To.Airport.ToUpper().Trim());
    }
    
    public static bool IsDateFormatValid(string dateTime)
    {
        return DateTime.TryParse(dateTime, out _);
    }
    
    public static bool IsDateValid(AddFlightRequest flight)
    {
        return !(DateTime.Parse(flight.DepartureTime) >= DateTime.Parse(flight.ArrivalTime) ||
               DateTime.Parse(flight.DepartureTime) < DateTime.Parse(flight.ArrivalTime).AddDays(-90));
    }
} 

