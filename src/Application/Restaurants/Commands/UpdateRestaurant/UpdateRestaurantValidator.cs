using FluentValidation;

namespace Application.Restaurants.Commands.UpdateRestaurant;

public class UpdateRestaurantValidator : AbstractValidator<UpdateRestaurantCommand>
{
    public UpdateRestaurantValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .Length(1, 100).WithMessage("Name must be between 1 and 100 characters.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .Length(1, 500).WithMessage("Description must be between 1 and 500 characters.");

        RuleFor(x => x.HasDelivery)
            .NotNull().WithMessage("Has delivery is required.");
    }
}
