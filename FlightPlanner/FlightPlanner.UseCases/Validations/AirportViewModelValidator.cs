using FlightPlanner.UseCases.Models;
using FluentValidation;

namespace FlightPlanner.UseCases.Validations;

public class AirportViewModelValidator : AbstractValidator<AirportViewModel>
{
    public AirportViewModelValidator()
    {
        RuleFor(request => request.Country)
            .NotEmpty()
            .WithMessage("No country provided");
        RuleFor(request => request.City)
            .NotEmpty()
            .WithMessage("No city provided");;
        RuleFor(request => request.Airport)
            .NotEmpty()
            .WithMessage("No airport provided");;
    }
}