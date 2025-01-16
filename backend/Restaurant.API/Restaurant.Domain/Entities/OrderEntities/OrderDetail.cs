using Restaurant.Domain.Commons;
using Restaurant.Domain.Entities.DrinkEntities;
using Restaurant.Domain.Entities.MealEntities;
using Restaurant.Domain.Entities.Products;

namespace Restaurant.Domain.Entities.OrderEntities;
public class OrderDetail : EntityBase
{
    public int OrderId { get; set; }
    public int? MealId { get; set; }
    public int? ProductId { get; set; }
    public int? DrinkId { get; set; }
    public int Quantity { get; set; }

    // Navigations
    public Order Order { get; set; }
    public Meal Meal { get; set; }
    public Drink Drink { get; set; }
    public Product Product { get; set; }
}
