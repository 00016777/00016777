using Microsoft.AspNetCore.Identity;

namespace Restaurant.Domain.Entities.Identity
{
    public class ApplicationRole : IdentityRole<int>
    {
        public bool Active { get; set; } = true;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedDate { get; set; }
    }

    public static class Roles
    {
        public const string Manager = "manager";

        public const string Suplier = "suplier";

        public const string Student = "student";
    }

    public static class RestaurantAdmin
    {
        public static List<string> Managers = new List<string>()
        {
            "Manager"
        };
    }
}
