using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.Models.Identities;
using Restaurant.Application.Models.OrderDTOs;
using Restaurant.Application.Services.OrderServices;

namespace Restaurant.API.Controllers.Orders;

public class OrdersController : BaseController
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet("GetAllOrder")]
    public async Task<IList<OrderDto>> GetAllOrders()
    {
        return await _orderService.GetAllOrderAsync();
    }

    [HttpGet("GetOrderByUser")]
    public async Task<IList<OrderDto>> GetOrdersByUser()
    {
        return await _orderService.GetOrdersByUserAsync(UserDto);
    }

    [HttpPost("SaveOrder")]
    public async Task<OrderDto> SaveOrder([FromBody] OrderDto order)
    {
       return await _orderService.SaveOrderAsync(order, UserDto);
    }

    [HttpPost("GetOrderedUsers")]
    public async Task<IList<UserDto>> OrderedUsers(List<int> ids)
    {
        return await _orderService.GetOrderedUsersAsync(ids);
    }

    [HttpGet("GetOrderById/{id}")]
    public async Task<OrderDto> GetOrderById(int id)
    {
        return await _orderService.GetOrderById(id);
    }
}
