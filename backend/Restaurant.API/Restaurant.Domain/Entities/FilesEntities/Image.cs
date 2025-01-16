using Restaurant.Domain.Commons;
using Restaurant.Domain.Entities.DrinkEntities;
using Restaurant.Domain.Entities.MealEntities;
using Restaurant.Domain.Entities.Products;

namespace Restaurant.Domain.Entities.FilesEntities;

public class Image : EntityBase
{
    public string Url { get; set; }
    public string Description { get; set; }

    // Meal Reference (Many-to-One relationship with Meal)
    public int? MealId { get; set; }
    public Meal Meal { get; set; }

    // Drink Reference (Many-to-One relationship with Drink)
    public int? DrinkId { get; set; }
    public Drink Drink { get; set; }

    public int? ProductId { get; set; }
    public Product Product { get; set; }
}
