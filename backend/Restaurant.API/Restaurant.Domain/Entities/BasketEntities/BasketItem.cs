using Restaurant.Domain.Commons;
using Restaurant.Domain.Entities.DrinkEntities;
using Restaurant.Domain.Entities.MealEntities;
using Restaurant.Domain.Entities.Products;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant.Domain.Entities.BasketEntities;

public class BasketItem : EntityBase
{
    public int BasketId { get; set; }
    public int? MealId { get; set; }
    public int? DrinkId { get; set; }
    public int? ProductId { get; set; }
    public int Quantity { get; set; }

    [NotMapped]
    public bool changed { get; set; }

    //Navigation
    public Basket Basket { get; set; }
    public Meal Meal { get; set; }
    public Drink Drink { get; set; }
    public Product Product { get; set; }
}
