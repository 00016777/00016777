using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.BasketEntities;
using Restaurant.Domain.Entities.DrinkEntities;
using Restaurant.Domain.Entities.FilesEntities;
using Restaurant.Domain.Entities.Identity;
using Restaurant.Domain.Entities.MealEntities;
using Restaurant.Domain.Entities.OrderEntities;
using Restaurant.Domain.Entities.Products;

namespace Restaurant.Infrastructure.DbContexts;
public partial class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }

    #region Images
    public DbSet<Image> Images { get; set; }
    #endregion

    #region Meal DbSets
    public DbSet<Meal> Meals { get; set; }
    #endregion

    #region Drink DbSets
    public DbSet<Drink> Drinks { get; set; }
    #endregion

    #region Order and OrderDetail
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }

    #endregion

    #region Basket and BasketItem
    public DbSet<Basket> Baskets { get; set; }
    public DbSet<BasketItem> BasketItems { get; set; }
    #endregion

    #region Product
    public DbSet<Product> Products { get; set; }
    #endregion
}
