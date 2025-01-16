using Restaurant.Domain.Commons;
using Restaurant.Domain.Entities.Identity;
using Restaurant.Domain.Enums;

namespace Restaurant.Domain.Entities.OrderEntities;

public class Order : EntityBase
{
    public int UserId { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    
    //Navigation
    public ApplicationUser User { get; set; }
    public ICollection<OrderDetail> OrderDetails { get; set; }
}
