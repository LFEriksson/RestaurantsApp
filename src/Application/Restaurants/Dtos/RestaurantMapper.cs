using Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace Application.Restaurants.Dtos;

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.None)]
public partial class RestaurantMapper
{

    [MapProperty(nameof(Restaurant.Address.StreetAdress), nameof(RestaurantDto.StreetAdress))]
    [MapProperty(nameof(Restaurant.Address.City), nameof(RestaurantDto.City))]
    [MapProperty(nameof(Restaurant.Address.ZipCode), nameof(RestaurantDto.ZipCode))]
    public partial RestaurantDto RestaurantToRestaurantDto(Restaurant entity);


    [MapProperty(nameof(RestaurantDto.StreetAdress), nameof(Restaurant.Address.StreetAdress))]
    [MapProperty(nameof(RestaurantDto.City), nameof(Restaurant.Address.City))]
    [MapProperty(nameof(RestaurantDto.ZipCode), nameof(Restaurant.Address.ZipCode))]
    public partial Restaurant CreateRestaurantDtoToRestaurant(CreateRestaurantDto dto);
}
