using Application.Dishes.Commands.CreateDish;
using Application.Dishes.Commands.DeleteDish;
using Application.Dishes.Dtos;
using Application.Dishes.Queries.GetDishByIdForRestaurant;
using Application.Dishes.Queries.GetDishesForRestaurant;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/v1/restaurant/{restaurantId}/dishes")]
public class DishesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<DishDto>))]
    public async Task<ActionResult<IEnumerable<DishDto>>> GetDishesForRestaurant([FromRoute] int restaurantId)
    {
        return Ok(await mediator.Send(new GetDishesForRestaurantQuery(restaurantId)));
    }

    [HttpGet("{dishId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DishDto))]
    public async Task<ActionResult<DishDto>> GetDishForRestaurant([FromRoute] int restaurantId, [FromRoute] int dishId)
    {
        return Ok(await mediator.Send(new GetDishByIdForRestaurantQuery(restaurantId, dishId)));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateDish([FromRoute] int restaurantId, CreateDishCommand command)
    {
        command.RestaurantId = restaurantId;
        var id = await mediator.Send(command);
        return CreatedAtAction(nameof(GetDishForRestaurant), new { restaurantId, id }, id);
    }

    [HttpDelete("{dishId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteDish([FromRoute] int restaurantId, [FromRoute] int dishId)
    {
        await mediator.Send(new DeleteDishCommand(restaurantId, dishId));
        return NoContent();
    }
}
