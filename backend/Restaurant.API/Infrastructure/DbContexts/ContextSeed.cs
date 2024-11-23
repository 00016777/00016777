﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Identity;

namespace Restaurant.Infrastructure.DbContexts
{
    public static class ContextSeed
    {
        public static void Seed(this ModelBuilder builder)
        {
            builder.SeedRoles();
            builder.SeedAuths();
        }

        private static void SeedRoles(this ModelBuilder builder)
        {
            builder.Entity<ApplicationRole>(roles =>
            {
                roles.HasData(new ApplicationRole
                {
                    Id = 1,
                    Name = Roles.Admin,
                    NormalizedName = Roles.Admin.ToUpper(),
                    ConcurrencyStamp = "1"
                }, new ApplicationRole
                {
                    Id = 2,
                    Name = Roles.Manager,
                    NormalizedName = Roles.Manager.ToUpper(),
                    ConcurrencyStamp = "2"
                }, new ApplicationRole
                {
                    Id = 3,
                    Name = Roles.Suplier,
                    NormalizedName = Roles.Suplier.ToUpper(),
                    ConcurrencyStamp = "3"
                }, new ApplicationRole
                {
                    Id = 4,
                    Name = Roles.Student,
                    NormalizedName = Roles.Student.ToUpper(),
                    ConcurrencyStamp = "4"
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
                    UserName = Roles.Admin,
                    NormalizedUserName = Roles.Admin.ToUpper(),
                    Email = "admin@gmail.com",
                    NormalizedEmail = "admin@gmail.com".ToUpper(),
                    EmailConfirmed = true,
                    PasswordHash = hashHelper.HashPassword(null!, "00016777"),
                    SecurityStamp = string.Empty
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
                       RoleId = 2,
                       UserId = 1,
                   });
        }

    }
}
