using Microsoft.EntityFrameworkCore;
using Restaurant.Application.Models.BasketDTOs;
using Restaurant.Application.Models.Identities;
using Restaurant.Domain.Entities.BasketEntities;
using Restaurant.Infrastructure.DbContexts;

namespace Restaurant.Application.Services.BasketServices;

public class BasketService : IBasketService
{
    private readonly AppDbContext _context;

    public BasketService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<BasketDto> GetBasketAsync(UserDto user)
    {
        var basket = await _context.Baskets
            .Include(b => b.BasketItems)
            .FirstOrDefaultAsync(b => b.UserId == user.Id && b.Active);

        if (basket == null)
            return new();

        return new()
        {
            Id = basket.Id,
            LastUpdated = basket.LastUpdated,
            BasketItems = basket.BasketItems
                .Where(x => x.Active)
                .Select(b => new BasketItemDto
                {
                    Id = b.Id,
                    BasketId = b.Id,
                    DrinkId = b.DrinkId,
                    MealId = b.MealId,
                    ProductId = b.ProductId,
                    Quantity = b.Quantity,

                }).ToList()
        };
    }

    public async Task<BasketDto> GetBasketByIdAsync(int id, UserDto user)
    {
        var basket = await _context.Baskets
            .Include(b => b.BasketItems)
            .FirstOrDefaultAsync(b => b.Id == id && b.Active);

        if (basket == null)
            return new();

        return new()
        {
            Id = basket.Id,
            LastUpdated = basket.LastUpdated,
            BasketItems = basket.BasketItems
                .Where(x => x.Active)
                .Select(b => new BasketItemDto
                {
                    Id = b.Id,
                    BasketId = b.Id,
                    DrinkId = b.DrinkId,
                    MealId = b.MealId,
                    ProductId = b.ProductId,
                    Quantity = b.Quantity,

                }).ToList()
        };
    }

    public async Task<BasketDto> GetBasketByUserIdAsync(int userId, UserDto user)
    {
        var basket = await _context.Baskets
            .Include(b => b.BasketItems)
            .FirstOrDefaultAsync(b => b.UserId == userId && b.Active);

        if (basket == null)
            return new();

        return new()
        {
            Id = basket.Id,
            LastUpdated = basket.LastUpdated,
            BasketItems = basket.BasketItems
                .Where(x => x.Active)
                .Select(b => new BasketItemDto
                {
                    Id = b.Id,
                    BasketId = b.Id,
                    DrinkId = b.DrinkId,
                    ProductId = b.ProductId,
                    MealId = b.MealId,
                    Quantity = b.Quantity,

                }).ToList()
        };
    }
    public async Task<BasketDto> SaveBasketAsync(BasketDto dto, UserDto user)
    {
        var isSaved = false;
        var basket = new Basket();
        if(dto.Id == 0)
        {
            basket = new Basket
            {
                UserId = user.Id,
                LastUpdated = DateTime.UtcNow,
                BasketItems = dto.BasketItems
                    .Select(bi => new BasketItem
                    {
                        DrinkId = bi.DrinkId,
                        ProductId = bi.ProductId,
                        MealId = bi.MealId,
                        Quantity = bi.Quantity,
                    }).ToList()

            };

            _context.Baskets.Add(basket);

            isSaved = await _context.SaveChangesAsync() > 0;
        }
        else
        {
            basket = await _context.Baskets
                .Include(b => b.BasketItems.Where(bi => bi.Active))
                .FirstOrDefaultAsync(b => 
                                     b.Active && 
                                     b.Id == dto.Id && 
                                     b.UserId == user.Id);

            var basketItemsToRemove = basket.BasketItems
                .Where( b => !dto.BasketItems.Any( x => x.Id == b.Id ));

            var basketItemsToAdd = dto.BasketItems
                .Where( b => b.Id == 0 )
                .ToList();

            var basketItemsToChange = basket.BasketItems
                .Where(b => dto.BasketItems.Any(x => x.Id == b.Id && x.changed));

            foreach (var item in basketItemsToRemove)
            {
                var itemToReplace = basketItemsToAdd
                    .FirstOrDefault(b =>
                                    b.MealId == item.MealId &&
                                    b?.DrinkId == item?.DrinkId && 
                                    b?.ProductId == item?.ProductId);

                if(itemToReplace is not null)
                {
                    item.Quantity = itemToReplace.Quantity;
                    basketItemsToAdd.Remove(itemToReplace);

                }
                else
                {
                    basket.BasketItems.Remove(item);
                }
            }

            foreach(var item in basketItemsToAdd)
            {
                basket.BasketItems.Add(new()
                {
                    MealId = item.MealId,
                    DrinkId = item.DrinkId,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                });
            }

            foreach(var item in basketItemsToChange)
            {
                var itemtochange = dto.BasketItems.FirstOrDefault(x => item.Id == x.Id);

                if (itemtochange is not null)
                {
                    item.Quantity = itemtochange.Quantity;
                }
            }

            _context.Baskets.Update(basket);
            isSaved = await _context.SaveChangesAsync() > 0;
        }

        return isSaved
            ? new BasketDto
            {
                Id = basket.Id,
                LastUpdated = DateTime.UtcNow,
                BasketItems = basket.BasketItems
                                .Where(x => x.Active)
                                .Select(bi => new BasketItemDto
                                {
                                    BasketId = bi.BasketId,
                                    ProductId = bi.ProductId,
                                    DrinkId = bi.DrinkId,
                                    MealId = bi.MealId,
                                    Quantity = bi.Quantity,
                                }).ToList()
            }
            : new();
    }

    public async Task<bool> SetInActiveBasketItems(int basketId)
    {
        var basketItems = await _context.BasketItems
            .Where(b => b.BasketId == basketId)
            .ToListAsync();

        foreach(var item in basketItems)
        {
            item.Active = false;
        }

        _context.BasketItems.UpdateRange(basketItems);

        var isSaved = await _context.SaveChangesAsync() > 0;

        return isSaved;
    }
}
