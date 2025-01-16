using Microsoft.EntityFrameworkCore;
using Restaurant.Application.Models.Identities;
using Restaurant.Application.Models.OrderDTOs;
using Restaurant.Domain.Entities.OrderEntities;
using Restaurant.Infrastructure.DbContexts;

namespace Restaurant.Application.Services.OrderServices;

public class OrderService : IOrderService
{
    private readonly AppDbContext _context;

    public OrderService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IList<OrderDto>> GetAllOrderAsync()
    {
        var orders = await _context.Orders
            .Include(o => o.OrderDetails)
            .Where(o => o.Active)
            .ToListAsync();

        return orders.Select(o => new OrderDto
        {
            Id = o.Id,
            Status = o.Status,
            UserId = o.UserId,
            Name = _context.Users.FirstOrDefault(x => x.Id == o.UserId)!.FullName,
            OrderDate = o.OrderDate,
            OrderDetailDtos = o.OrderDetails
               .Select(od => new OrderDetailDto
               {
                   Id = od.Id,
                   MealId = od.MealId,
                   OrderId = od.OrderId,
                   ProductId = od.ProductId,
                   Quantity = od.Quantity,
               }).ToList(),
        }).ToList();
    }

    public async Task<OrderDto> GetOrderById(int id)
    {
        var order = await _context
            .Orders
            .Include(o => o.OrderDetails)
            .Where(o => o.Id == id && o.Active)
            .FirstOrDefaultAsync();

        return new OrderDto
        {
            Id = order.Id,
            Status = order.Status,
            UserId = order.UserId,
            OrderDate = order.OrderDate,
            Name = _context.Users.FirstOrDefault(x => x.Id == order.UserId)!.FullName,
            OrderDetailDtos = order.OrderDetails
                .Select(o => new OrderDetailDto
                {
                    Id = o.Id,
                    MealId = o.MealId,
                    OrderId = o.OrderId,
                    ProductId = o.ProductId,
                    Quantity = o.Quantity
                }).ToList(),
        };
    }

    public async Task<IList<UserDto>> GetOrderedUsersAsync(List<int> ids)
    {
        var users = await _context.Users
            .Where(x => ids.Contains(x.Id))
            .ToListAsync();

        return users.Select(u => 
            new UserDto
            {
                Id = u.Id,
                FullName = u.FullName,
                ImageUrl = u.ImageUrl,
            }).ToList();
    }

    public async Task<IList<OrderDto>> GetOrdersByUserAsync(UserDto user)
    {
        var orders = await _context.Orders
            .Include(o => o.OrderDetails)
            .Where(o => o.UserId == user.Id && o.Active)
            .ToListAsync();

        return orders.Select(o => new OrderDto
        {
            Id = o.Id,
            Status = o.Status,
            UserId = o.UserId,
            Name = _context.Users.FirstOrDefault(x => x.Id == o.UserId)!.FullName,
            OrderDate = o.OrderDate,
            OrderDetailDtos = o.OrderDetails
               .Select(od => new OrderDetailDto
               {
                   Id = od.Id,
                   MealId = od.MealId,
                   OrderId = od.OrderId,
                   ProductId = od.ProductId,
                   Quantity = od.Quantity,
               }).ToList(),
        }).ToList();
    }

    public async Task<OrderDto> SaveOrderAsync(OrderDto orderDto, UserDto user)
    {
        var isSaved = false;
        var order = new Order();
        if (orderDto.Id == 0)
        {
            order = new Order
            {
                OrderDate = DateTime.UtcNow,
                Status = Domain.Enums.OrderStatus.Pending,
                UserId = user.Id,
                OrderDetails = orderDto.OrderDetailDtos
                    .Select(o => new OrderDetail
                    {
                        MealId = o.MealId,
                        ProductId = o.ProductId,
                        Quantity = o.Quantity,
                    }).ToList(),
            };

            _context.Orders.Add(order);

            isSaved = await _context.SaveChangesAsync() > 0;
        }
        else
        {
            order = await _context.Orders
                .Include(o => o.OrderDetails)
                .FirstOrDefaultAsync(o => o.Id == orderDto.Id);

            if (order is null)
                return new();

            order.Status = orderDto.Status;

            _context.Orders.Update(order);
            isSaved = await _context.SaveChangesAsync() > 0;
        }

        return isSaved
            ? new OrderDto
            {
                Id = orderDto.Id,
                Status = order.Status,
                UserId = order.UserId,
                OrderDate = orderDto.OrderDate,
                Name = _context.Users.FirstOrDefault(x => x.Id == order.UserId)!.FullName,
                OrderDetailDtos = order.OrderDetails
                    .Select(o => new OrderDetailDto
                    {
                        Id = o.Id,
                        MealId = o.MealId,
                        OrderId = o.OrderId,
                        ProductId = o.ProductId,
                        Quantity = o.Quantity
                    }).ToList(),
            }
            : new();
    }
}
