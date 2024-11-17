using Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace Application.Restaurants.Dtos;

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.None)]
public partial class RestaurantMapper
{

    [MapProperty(nameof(Restaurant.Address.Streetaddress), nameof(RestaurantDto.StreetAddress))]
    [MapProperty(nameof(Restaurant.Address.City), nameof(RestaurantDto.City))]
    [MapProperty(nameof(Restaurant.Address.ZipCode), nameof(RestaurantDto.ZipCode))]
    public partial RestaurantDto RestaurantToRestaurantDto(Restaurant entity);


    [MapProperty(nameof(RestaurantDto.StreetAddress), nameof(Restaurant.Address.Streetaddress))]
    [MapProperty(nameof(RestaurantDto.City), nameof(Restaurant.Address.City))]
    [MapProperty(nameof(RestaurantDto.ZipCode), nameof(Restaurant.Address.ZipCode))]
    public partial Restaurant CreateRestaurantDtoToRestaurant(CreateRestaurantDto dto);
}
