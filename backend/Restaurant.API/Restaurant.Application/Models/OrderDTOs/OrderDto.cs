using Restaurant.Domain.Enums;

namespace Restaurant.Application.Models.OrderDTOs;

public class OrderDto
{
    public int Id { get; set; }
    public OrderStatus Status { get; set; }
    public DateTime OrderDate { get; set; }
    public int? UserId { get; set; }
    public string? Name { get; set; }
    public List<OrderDetailDto> OrderDetailDtos { get; set; }
}
