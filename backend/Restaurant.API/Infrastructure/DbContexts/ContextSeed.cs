using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.DrinkEntities;
using Restaurant.Domain.Entities.Identity;
using Restaurant.Domain.Entities.MealEntities;
using System.Reflection.Emit;

namespace Restaurant.Infrastructure.DbContexts
{
    public static class ContextSeed
    {
        public static void Seed(this ModelBuilder builder)
        {
            builder.SeedRoles();
            builder.SeedAuths();
            builder.SeedMeals();
            builder.SeedDrinks();
        }

        private static void SeedRoles(this ModelBuilder builder)
        {
            builder.Entity<ApplicationRole>(roles =>
            {
                roles.HasData(new ApplicationRole
                {
                    Id = 1,
                    Name = Roles.Manager,
                    NormalizedName = Roles.Manager.ToUpper(),
                    ConcurrencyStamp = "1"
                }, new ApplicationRole
                {
                    Id = 2,
                    Name = Roles.Suplier,
                    NormalizedName = Roles.Suplier.ToUpper(),
                    ConcurrencyStamp = "2"
                }, new ApplicationRole
                {
                    Id = 3,
                    Name = Roles.Student,
                    NormalizedName = Roles.Student.ToUpper(),
                    ConcurrencyStamp = "3"
                });
            });
        }

        private static void SeedAuths(this ModelBuilder builder)
        {
            var hashHelper = new PasswordHasher<ApplicationUser>();

            builder.Entity<ApplicationUser>(users =>
            {
                users.HasData(new ApplicationUser
                {
                    Id = 1,
                    UserName = "Hayitbek",
                    MainRoleId = 1,
                    FullName = "Hayitbek MIrsoatov",
                    NormalizedUserName = "Hayitbek".ToUpper(),
                    Email = "wuit00016777@gmail.com",
                    NormalizedEmail = "wuit00016777@gmail.com".ToUpper(),
                    EmailConfirmed = true,
                    PasswordHash = hashHelper.HashPassword(null!, "00016777"),
                    SecurityStamp = "1"    
                });
            });

            builder.Entity<IdentityUserRole<int>>()
                   .HasData(new IdentityUserRole<int>
                   {
                       RoleId = 1,
                       UserId = 1,
                   },
                   new IdentityUserRole<int>
                   {
                       RoleId = 3,
                       UserId = 1
                   });
        }

        private static void SeedMeals(this ModelBuilder builder) 
        {
            builder.Entity<Meal>().HasData(
                new Meal { Id = 1, IsCategory = true, Name = "Osh", Price = null },
                new Meal { Id = 2, IsCategory = true, Name = "Somsa", Price = null },
                new Meal { Id = 3, IsCategory = false, Name = "Cheeseburger", Price = 30000 },
                new Meal { Id = 4, IsCategory = true, ParentId = 1, Name = "Samarkand Osh" },
                new Meal { Id = 5, IsCategory = true, ParentId = 1, Name = "Andijon Osh" },
                new Meal { Id = 6, IsCategory = false, ParentId = 2, Name = "Tandir Somsa" },
                new Meal { Id = 7, IsCategory = false, ParentId = 4, Name = "To'y Osh" },
                new Meal { Id = 8, IsCategory = false, ParentId = 5, Name = "Choyxona Osh" }
            );
        }
        
        private static void SeedDrinks(this ModelBuilder builder)
        {
            // Seed Drinks
            builder.Entity<Drink>().HasData(
                new Drink { 
                    Id = 1, 
                    Name = "Cola",
                    Description = "cola cola",
                    Price = null },
                new Drink { 
                    Id = 2, 
                    Name = "Pepsi",
                    Description = "pepsi pepsi",
                    Price = null },

                new Drink { Id = 3, IsCategory = true, Price = null, ParentId = 1, Name = "Can" },
                new Drink { Id = 4, IsCategory = true, Price = null, ParentId = 2, Name = "Bottle" },
                new Drink { Id = 5, IsCategory = false, Price = 12000, ParentId = 1, Name = "500ml" },
                new Drink { Id = 6, IsCategory = false, Price = 11000, ParentId = 2, Name = "1L" }
            );
        }
    }
}
