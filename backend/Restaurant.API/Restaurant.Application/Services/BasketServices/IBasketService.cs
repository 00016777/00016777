using Restaurant.Application.Models.BasketDTOs;
using Restaurant.Application.Models.Identities;

namespace Restaurant.Application.Services.BasketServices;

public interface IBasketService
{
    Task<BasketDto> SaveBasketAsync(BasketDto dto, UserDto user);
    Task<BasketDto> GetBasketAsync(UserDto user);
    Task<BasketDto> GetBasketByIdAsync(int id, UserDto user);
    Task<BasketDto> GetBasketByUserIdAsync(int userId, UserDto user);
    Task<bool> SetInActiveBasketItems(int basketId);
}
