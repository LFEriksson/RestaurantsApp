using Application.Restaurants;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.ObjectModel;

namespace API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class RestaurantsController(IRestaurantsService restaurantsService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var restaurants = await restaurantsService.GetAllRestaurants();
        return Ok(restaurants);
    }
}
