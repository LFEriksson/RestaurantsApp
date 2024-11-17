using Domain.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Restaurants.Commands.CreateRestaurantCommand;

internal class CreateRestaurantCommandHandler(
    IRestaurantsRepository restaurantsRepository,
    ILogger<CreateRestaurantCommandHandler> logger,
    IValidator<CreateRestaurantCommand> validator) : IRequestHandler<CreateRestaurantCommand, int>
{
    public async Task<int> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating restaurant");
        var validationResult = validator.Validate(request);
        if (!validationResult.IsValid)
        {
            logger.LogWarning("Create restaurant dto is not valid");
            throw new ValidationException(validationResult.Errors);
        }
        var mapper = new RestaurantMapper();
        var restaurant = mapper.CreateRestaurantCommandToRestaurant(request);
        return await restaurantsRepository.AddAsync(restaurant);
    }

}
