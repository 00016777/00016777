using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Commons;
using Restaurant.Domain.Entities.BasketEntities;
using Restaurant.Domain.Entities.DrinkEntities;
using Restaurant.Domain.Entities.FilesEntities;
using Restaurant.Domain.Entities.MealEntities;
using Restaurant.Domain.Entities.OrderEntities;
using Restaurant.Domain.Entities.Products;

namespace Restaurant.Infrastructure.DbContexts
{
    partial class AppDbContext
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(ContextSeed).Assembly).Seed();

            #region schema configuration
            // Schema setting
            builder.Entity<Meal>().ToTable("Meals", "restaurant");
            builder.Entity<Drink>().ToTable("Drinks", "restaurant");
            builder.Entity<Image>().ToTable("Images", "restaurant");
            builder.Entity<Order>().ToTable("Orders", "restaurant");
            builder.Entity<OrderDetail>().ToTable("OrderDetails", "restaurant");
            builder.Entity<Basket>().ToTable("baskets", "restaurant");
            builder.Entity<BasketItem>().ToTable("basketItems", "restaurant");
            builder.Entity<Product>().ToTable("Products", "restaurant");
            #endregion

            #region Meal
            builder.Entity<Meal>(e =>
            {
                e.HasKey(e => e.Id);

                //Self-referencing relationship for parent child hierarchy
                e.HasOne(mt => mt.Parent)
                 .WithMany(m => m.Children)
                 .HasForeignKey(mt => mt.ParentId)
                 .OnDelete(DeleteBehavior.Cascade);

            });
            #endregion

            #region Drink
            builder.Entity<Drink>(e =>
            {
                e.Property(d => d.Price)
                 .HasColumnType("decimal(10, 2)");

                // Self-referencing relationship for parent-child hierarchy
                e.HasOne(d => d.Parent)
                 .WithMany(d => d.Children)
                 .HasForeignKey(d => d.ParentId)
                 .OnDelete(DeleteBehavior.Cascade);
            });
            #endregion

            #region Image

            builder.Entity<Image>(e =>
            {
                // Primary Key
                e.HasKey(i => i.Id);

                // Properties
                e.Property(i => i.Url)
                    .IsRequired()
                    .HasMaxLength(500); // Adjust max length as needed

                e.Property(i => i.Description)
                    .HasMaxLength(1000); // Optional, adjust as needed

                // Relationships
                e.HasOne(i => i.Meal)
                    .WithMany(m => m.Images) // Assuming Meal has a collection of Images
                    .HasForeignKey(i => i.MealId)
                    .OnDelete(DeleteBehavior.Cascade); // Avoid cascade delete if needed

                e.HasOne(i => i.Drink)
                    .WithMany(d => d.Images) // Assuming Drink has a collection of Images
                    .HasForeignKey(i => i.DrinkId)
                    .OnDelete(DeleteBehavior.Cascade); // Avoid cascade delete if needed
            });
            #endregion

            #region Order and OrderDetail
            builder.Entity<Order>(e =>
            {
                // Relationships
                e.HasOne(o => o.User)
                    .WithMany(u => u.Orders)
                    .HasForeignKey(o => o.UserId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            builder.Entity<OrderDetail>(e =>
            {
                // Relationships
                e.HasOne(od => od.Order)
                    .WithMany(o => o.OrderDetails)
                    .HasForeignKey(od => od.OrderId)
                    .OnDelete(DeleteBehavior.Cascade); 

                e.HasOne(od => od.Meal)
                    .WithMany(m => m.OrderDetails)
                    .HasForeignKey(od => od.MealId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
            #endregion

            #region Basket and BasketItem
            builder.Entity<Basket>(e =>
            {
                // Relationships
                e.HasOne(b => b.User)
                    .WithMany(u => u.Baskets)
                    .HasForeignKey(b => b.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<BasketItem>(e =>
            {
                // Relationships
                e.HasOne(bi => bi.Basket)
                    .WithMany(b => b.BasketItems)
                    .HasForeignKey(bi => bi.BasketId)
                    .OnDelete(DeleteBehavior.Cascade); 

                e.HasOne(bi => bi.Meal)
                    .WithMany(m => m.BasketItems)
                    .HasForeignKey(bi => bi.MealId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
            #endregion
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var modifiedEntities = ChangeTracker.Entries()
                    .Where(entity => entity.State == EntityState.Modified)
                    .ToList();

            foreach(var entity in modifiedEntities)
            {
                entity.Property("UpdatedDate").CurrentValue = DateTime.UtcNow;
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
