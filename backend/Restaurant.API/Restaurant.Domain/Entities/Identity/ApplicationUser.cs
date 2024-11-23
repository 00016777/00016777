using Microsoft.AspNetCore.Identity;

namespace Restaurant.Domain.Entities.Identity
{
    public class ApplicationUser : IdentityUser<int>
    {
        public DateTime LastActive { get; set; }

        public int CountOfEnter { get; set; }

        public int? MainRoleId { get; set; }

        public string FullName { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;

        public void UpdateActive()
        {
            LastActive = DateTime.UtcNow;
            CountOfEnter += 1;
        }
    }
}
