using Restaurant.Domain.Commons;
using Restaurant.Domain.Entities.Identity;

namespace Restaurant.Domain.Entities.BasketEntities;

public class Basket : EntityBase
{
    public int UserId { get; set; }
    public DateTime LastUpdated { get; set; }

    //Navigation
    public ApplicationUser User { get; set; }
    public ICollection<BasketItem> BasketItems { get; set; }
}
