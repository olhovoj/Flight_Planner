using FlightPlanner.UseCases.Models;
using FlightPlanner.UseCases.Validations;
using FluentValidation;

namespace FlightPlanner.Validations;

public class AddFlightRequestValidator : AbstractValidator<AddFlightRequest>
{
    public AddFlightRequestValidator()
    {
        RuleFor(request => request.Carrier)
            .NotEmpty()
            .WithMessage("No carrier provided");
        RuleFor(request => request.DepartureTime)
            .NotEmpty()
            .Must(ArgumentValidation.IsDateFormatValid)
            .WithMessage("Invalid date format provided");
        RuleFor(request => request.ArrivalTime)
            .NotEmpty()
            .Must(ArgumentValidation.IsDateFormatValid)
            .WithMessage("Invalid date format provided");
        RuleFor(request => request.To)
            .SetValidator(new AirportViewModelValidator());
        RuleFor(request => request.From)
            .SetValidator(new AirportViewModelValidator());
        
        RuleFor(request => request)
            .Must(ArgumentValidation.IsDifferentFromToAirport)
            .WithMessage("Same airports provided");
        
        RuleFor(request => request)
            .Must(ArgumentValidation.IsDateValid)
            .WithMessage("Invalid departure or arrival date provided");
    }
}