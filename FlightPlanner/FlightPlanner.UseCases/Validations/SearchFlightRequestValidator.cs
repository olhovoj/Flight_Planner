using FlightPlanner.Core.Models;
using FluentValidation;

namespace FlightPlanner.UseCases.Validations;

public class SearchFlightRequestValidator : AbstractValidator<SearchFlightsRequest>
{
    public SearchFlightRequestValidator()
    {
        RuleFor(request => 
            request.From + 
            request.To + 
            request.DepartureDate)
            .NotEmpty()
            .WithMessage("No searching information provided");
        RuleFor(request => request)
            .Must(request => request.From != request.To)
            .WithMessage("Same airports provided.");
    }
}