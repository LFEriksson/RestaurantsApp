using Application.Restaurants.Dtos;
using FluentValidation;

namespace Application.Restaurants.Validators;

public class CreateRestaurantDtoValidator : AbstractValidator<CreateRestaurantDto>
{
    public CreateRestaurantDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .Length(1, 100).WithMessage("Name must be between 1 and 100 characters.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .Length(1, 500).WithMessage("Description must be between 1 and 500 characters.");

        RuleFor(x => x.Category)
            .NotEmpty().WithMessage("Category is required.")
            .Length(1, 100).WithMessage("Category must be between 1 and 100 characters.");

        RuleFor(x => x.StreetAddress)
            .NotEmpty().WithMessage("Street address is required.")
            .Length(1, 200).WithMessage("Street address must be between 1 and 200 characters.");

        RuleFor(x => x.City)
            .NotEmpty().WithMessage("City is required.")
            .Length(1, 100).WithMessage("City must be between 1 and 100 characters.");

        RuleFor(x => x.ZipCode)
            .NotEmpty().WithMessage("Zip code is required.")
            .Length(1, 18).WithMessage("Zip code must be between 1 and 18 characters.");

        RuleFor(x => x.ContactEmail)
            .NotEmpty().WithMessage("Contact email is required.")
            .EmailAddress().WithMessage("Contact email must be a valid email address.")
            .Length(1, 100).WithMessage("Contact email must be between 1 and 100 characters.");

        RuleFor(x => x.ContactNumber)
            .NotEmpty().WithMessage("Contact number is required.")
            .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Contact number must be a valid phone number.")
            .Length(1, 20).WithMessage("Contact number must be between 1 and 20 characters.");

        RuleFor(x => x.HasDelivery)
            .NotNull().WithMessage("Has delivery is required.");
    }
}
