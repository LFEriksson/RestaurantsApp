using Application.Restaurants.Dtos;
using FluentValidation;

namespace Application.Restaurants.Queries.GetAllRestaurants;

public class GetAllRestaurantsQueryValidator : AbstractValidator<GetAllRestaurantsQuery>
{
    private string[] allowedSortByColumnNames = [nameof(RestaurantDto.Name),
    nameof(RestaurantDto.Category),
    nameof(RestaurantDto.Description)];
    public GetAllRestaurantsQueryValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThan(0)
            .WithMessage("Page number must be greater than 0");

        RuleFor(x => x.PageSize)
            .GreaterThan(0)
            .LessThanOrEqualTo(100)
            .WithMessage("Page size must be between 1 and 100");

        RuleFor(x => x.SearchPhrase)
            .MaximumLength(100)
            .WithMessage("Search phrase must be less than 100 characters");
        RuleFor(x => x.SortBy)
            .Must(value => allowedSortByColumnNames.Contains(value))
            .When(x => x.SortBy != null)
            .WithMessage($"Sort by is optional, or must be one of the following: {string.Join(", ", allowedSortByColumnNames)}");
    }
}
