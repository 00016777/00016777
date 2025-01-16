using Restaurant.Domain.Commons;
using Restaurant.Domain.Entities.BasketEntities;
using Restaurant.Domain.Entities.FilesEntities;
using Restaurant.Domain.Entities.OrderEntities;

namespace Restaurant.Domain.Entities.Products;

public class Product : EntityBase
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal PricePerKG { get; set; }
    public ICollection<Image> Images { get; set; }
    public ICollection<OrderDetail> OrderDetails { get; set; }
    public ICollection<BasketItem> BasketItems { get; set; }

}
