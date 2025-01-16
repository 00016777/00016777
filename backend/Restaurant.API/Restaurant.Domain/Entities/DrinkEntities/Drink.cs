using Restaurant.Domain.Commons;
using Restaurant.Domain.Entities.FilesEntities;

namespace Restaurant.Domain.Entities.DrinkEntities;

public class Drink : EntityBase
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public int? ParentId { get; set; }
    public bool IsCategory { get; set; }

    //Navigations
    public Drink Parent { get; set; }
    public decimal? Price { get; set; }
    public ICollection<Drink> Children { get; set; }
    public ICollection<Image> Images { get; set; }

}
