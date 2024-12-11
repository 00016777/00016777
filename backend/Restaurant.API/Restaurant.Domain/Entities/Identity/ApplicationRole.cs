using Microsoft.AspNetCore.Identity;

namespace Restaurant.Domain.Entities.Identity
{
    public class ApplicationRole : IdentityRole<int>
    {
    }

    public static class Roles
    {
        public const string Admin = "admin";

        public const string Manager = "manager";

        public const string Suplier = "suplier";

        public const string Student = "student";
    }

    public static class RestaurantAdmin
    {
        public static List<string> Admins = new List<string>()
        {
            "Admin"
        };
    }
}
