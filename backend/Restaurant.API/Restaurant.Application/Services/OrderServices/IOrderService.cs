using Restaurant.Application.Models.Identities;
using Restaurant.Application.Models.OrderDTOs;

namespace Restaurant.Application.Services.OrderServices;

public interface IOrderService
{
    Task<OrderDto> SaveOrderAsync(OrderDto orderDto, UserDto user);

    Task<IList<OrderDto>> GetOrdersByUserAsync(UserDto user);

    Task<IList<OrderDto>> GetAllOrderAsync();

    Task<IList<UserDto>> GetOrderedUsersAsync(List<int> ids);

    Task<OrderDto> GetOrderById(int id);
}
