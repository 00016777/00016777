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
}
