using Microsoft.AspNetCore.Identity;
using Restaurant.Domain.Entities.BasketEntities;
using Restaurant.Domain.Entities.OrderEntities;

namespace Restaurant.Domain.Entities.Identity
{
    public class ApplicationUser : IdentityUser<int>
    {
        public DateTime LastActive { get; set; }

        public int CountOfEnter { get; set; }

        public int? MainRoleId { get; set; }

        public string FullName { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;

        public bool Active { get; set; } = true;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedDate { get; set; }

        public ICollection<Order> Orders { get; set; }

        public ICollection<Basket> Baskets { get; set; }

        public void UpdateActive()
        {
            LastActive = DateTime.UtcNow;
            CountOfEnter += 1;
        }
    }
}
