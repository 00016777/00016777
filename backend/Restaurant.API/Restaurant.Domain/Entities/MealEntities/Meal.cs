using Restaurant.Domain.Commons;
using Restaurant.Domain.Entities.BasketEntities;
using Restaurant.Domain.Entities.FilesEntities;
using Restaurant.Domain.Entities.OrderEntities;

namespace Restaurant.Domain.Entities.MealEntities;

public class Meal : EntityBase
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int? ParentId { get; set; }
    public bool IsCategory { get; set; }
    public decimal? Price { get; set; }

    //Navigations
    public Meal Parent { get; set; }
    public ICollection<Meal> Children { get; set; }
    public ICollection<Image> Images { get; set; }
    public ICollection<OrderDetail> OrderDetails { get; set; }
    public ICollection<BasketItem> BasketItems { get; set; }
}
