using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Restaurant.Application.Models.Identities;
using Restaurant.Application.Models.MealDTOs;
using Restaurant.Application.Services.FileServices;
using Restaurant.Domain.Commons;
using Restaurant.Domain.Entities.FilesEntities;
using Restaurant.Domain.Entities.Identity;
using Restaurant.Domain.Entities.MealEntities;
using Restaurant.Domain.Extensions;
using Restaurant.Infrastructure.DbContexts;

namespace Restaurant.Application.Services.MealServices;

public class MealService : IMealService
{
    private readonly AppDbContext _context;
    private readonly IFileService _fileService;

    public MealService(AppDbContext context, IFileService fileService)
    {
        _context = context;
        _fileService = fileService;
    }

    public async Task<MealDto> CreateMealAsync(MealDto mealDto, UserDto user)
    {
        if (mealDto == null) throw new ArgumentNullException(nameof(mealDto));

        var role = await _context.Roles
            .FirstOrDefaultAsync(x => x.Id == user.MainRoleId);

        if (role?.Name != Roles.Manager)
            throw new Exception("Only Manager can create meals");

        var newMeal = new Meal()
        {
            Name = mealDto.Name,
            Description = mealDto.Description,
            Price = mealDto?.Price,
            ParentId = mealDto?.ParentId,
            IsCategory = mealDto.IsCategory
        };

        _context.Meals.Add(newMeal);
        var isSaved = await _context.SaveChangesAsync() > 0;

        return await Task.FromResult(isSaved ? new MealDto
        {
            Id = newMeal.Id,
            Name = newMeal.Name,
            Description = newMeal.Description,
            Price = newMeal.Price,
            ParentId = newMeal.ParentId,
            IsCategory = newMeal.IsCategory
        } : null!);
    }

    public async Task<bool> DeleteMealAsync(int id, UserDto user)
    {
        if (id <= 0)
            return await Task.FromResult(false);

        var role = await _context.Roles
            .FirstOrDefaultAsync(x => x.Id == user.MainRoleId);

        if (role?.Name != Roles.Manager)
            throw new Exception("Only Manager can delete meal");

        var meal = await _context.Meals
            .Include(m => m.Images)
            .FirstOrDefaultAsync(m => m.Id == id);

        foreach (var image in meal.Images) 
        {
            await _fileService.DeleteFile(image.Url, "meals");
        }

        if (meal == null)
            return true;

        meal.Active = false;
        //_context.Meals.Remove(meal);

        var isDeleted = await _context.SaveChangesAsync() > 0;

        return isDeleted;
    }

    public async Task<IList<MealDto>> GetAllMealsAsync(Filter filter, UserDto user)
    {
        var query = _context.Meals
            .Where(m => m.Active)
            .Include(m => m.Children)
            .Include(m => m.Images)
            .AsNoTracking();

        var expression = new MealExpression();

        query = query.ApplyFiltering(filter, expression.Filter)
            .ApplyOrdering(filter, "Id", expression.Sort());

        query = query.Select(q => new Meal()
        {
            Id = q.Id,
            Name = q.Name,
            Price = q.Price,
            Description = q.Description,
            Images = q.Images,
            Children = q.Children,
            Active = q.Active,
            CreatedDate = q.CreatedDate,
            UpdatedDate = q.UpdatedDate
        });

        var meals = await query.ToListAsync();

        return meals.Select(m => new MealDto
        {
            Id = m.Id,
            Name = m.Name,
            Price = m.Price,
            Description = m.Description,
            ParentId = m.ParentId,
            Children = MapToDto(m.Children),
            Images = m.Images
                .Where(m => m.Active)
                .Select(im => im.Url)
                .ToList(),
        }).ToList();
    }

    public async Task<MealDtos> GetMealsAndChildrenAsync(int? parentId, string search = "")
    {
        // Step 1: Get all meals
        var meals = new List<Meal>();

        if (parentId == 0)
            parentId = null;

        var mealsQuery = _context.Meals
        .Include(m => m.Children)
        .Include(m => m.Images)
        .Where(m => m.ParentId == null && m.Active && m.IsCategory && 
        (string.IsNullOrEmpty(search) || m.Name.ToLower().Contains(search.ToLower())));

        if (parentId != null) {

            mealsQuery = _context.Meals
            .Include(m => m.Children)
            .Include(m => m.Images)
            .Where(
                m => 
                m.ParentId == parentId && 
                m.Active &&
                m.IsCategory && 
                (string.IsNullOrEmpty(search) || m.Name.ToLower().Contains(search.ToLower())));
;
        }

        meals = await mealsQuery.ToListAsync();

        var mealChildren = new List<Meal>();

        var mealChildrenQuery = _context.Meals
            .Include(m => m.Images)
            .Where(
                m => 
                m.Active &&
                m.IsCategory == false   &&
                (string.IsNullOrEmpty(search) || m.Name.ToLower().Contains(search.ToLower())));

        var parentIds = new List<int>() { (parentId ?? 0) };
        
        if(parentId is not null)
        {

            parentIds.AddRange(meals.Select(x => x.Id));

            mealChildrenQuery = _context.Meals
            .Include(m => m.Images)
            .Where(
                 m => parentIds.Any(x => x == m.ParentId) &&
                 m.Active &&
                 m.IsCategory == false &&
                 (string.IsNullOrEmpty(search) || m.Name.ToLower().Contains(search.ToLower())));
        }

        mealChildren = await mealChildrenQuery.ToListAsync();

        // Step 3: Prepare breadcrumbs (path)

        var path = new List<MealDto>();
        if(parentId is not null)
        {
            var currentMeal = await _context.Meals
                .Include(x => x.Images)
                .FirstOrDefaultAsync(x => x.Id == parentId && x.Active);

            while (currentMeal != null)
            {
                //meals.Remove(currentMeal);
                path.Insert(0, new MealDto
                {
                    Id = currentMeal.Id,
                    Name = currentMeal.Name,
                    Description = currentMeal.Description!,
                    ParentId = currentMeal.ParentId,
                    IsCategory = currentMeal.IsCategory,
                    Images = currentMeal.Images.Select(m => m.Url).ToList(),
                });

                if(currentMeal.ParentId is not null)
                {
                    currentMeal = await _context.Meals
                        .Include(x => x.Images)
                        .FirstOrDefaultAsync(mt => mt.Id == currentMeal.ParentId && mt.Active);
                }
                else
                {
                    currentMeal = null;
                }
            }
        }


        // Step 4: Prepare response
        return new MealDtos
        {
            Meals = meals
            .Select(m => new MealDto
            {
                Id = m.Id,
                Name = m.Name,
                Description = m.Description,
                Price = m.Price,
                ParentId = m?.ParentId,
                IsCategory = m.IsCategory,
                Images = m.Images
                    ?.Select(img => img.Url)
                    .ToList()!
            }).ToList(),

            MealChildren = mealChildren
            .Select(m => new MealDto
            {
                Id = m.Id,
                Name = m.Name,
                Description = m.Description,
                Price = m.Price,
                ParentId = m?.ParentId,
                IsCategory = m.IsCategory,
                Images = m.Images
                    ?.Select(img => img.Url)
                    .ToList()!
            }).ToList(),

            Path = path
        };
    }

    public async Task<MealDto> GetMealByIdAsync(int id, UserDto user)
    {
        if (id == 0)
            throw new Exception("Invalid meal Id");

        var meal = await _context.Meals
            .Include(m => m.Children)
            .Include(m => m.Images)
            .FirstOrDefaultAsync(m => m.Id == id && m.Active);

        if (meal == null)
            throw new Exception("Meal not found");

        return new MealDto
        {
            Id = id,
            Name = meal.Name,
            Price = meal.Price,
            Description = meal.Description!,
            IsCategory = meal.IsCategory,
            Images = meal.Images
                .Where(m => m.Active)
                .Select(im => im.Url).ToList(),
            Children = MapToDto(meal.Children)
        };
    }

    public async Task<MealDto> UpdateMealAsync(MealDto mealDto, UserDto user)
    {
        if (mealDto == null) throw new ArgumentNullException(nameof(mealDto));

        var role = await _context.Roles
            .FirstOrDefaultAsync(x => x.Id == user.MainRoleId);

        if (role?.Name != Roles.Manager)
            throw new Exception("Only Manager can create meals");

        var existinMeal = await _context.Meals
            .Include(m => m.Images)
            .Include(m => m.Children)
            .FirstOrDefaultAsync(x => x.Active && x.Id == mealDto.Id);

        if (existinMeal == null)
            throw new Exception("Meal not found !");

        existinMeal.Name = mealDto.Name;
        existinMeal.Description = mealDto.Description;
        existinMeal.Price = mealDto.Price;
        existinMeal.ParentId = mealDto?.ParentId;
        existinMeal.IsCategory = mealDto.IsCategory;
        existinMeal.Children = MapToMeal(mealDto.Children);

        _context.Meals.Update(existinMeal);
        var isSaved = await _context.SaveChangesAsync() > 0;

        return isSaved
            ? new MealDto()
            {
                Id = existinMeal.Id,
                Name = existinMeal.Name,
                Description = existinMeal.Description,
                IsCategory = existinMeal.IsCategory,
                Price = existinMeal.Price,
                Images = existinMeal.Images
                .Where(m => m.Active)
                .Select(im => im.Url).ToList(),
                Children = MapToDto(existinMeal.Children)
            }
            : throw new Exception("Updating Meal failed");
    }

    private List<Meal> MapToMeal(IEnumerable<MealDto> children)
    {
        if (children == null)
        {
            return new List<Meal>();
        }

        return children.Select(mt => new Meal
        {
            Id = mt.Id,
            Name = mt.Name,
            Description = mt.Description,
            Price = mt?.Price,
            ParentId = mt?.ParentId,
            IsCategory = mt.IsCategory,
            Children = mt.Children != null && mt.Children.Any()
                ? MapToMeal(mt.Children) // Recursively map children
                : new List<Meal>()
        }).ToList();
    }

    private List<MealDto> MapToDto(IEnumerable<Meal> children)
    {
        if (children == null)
        {
            return new List<MealDto>();
        }

        return children.Select(mt => new MealDto
        {
            Id = mt.Id,
            Name = mt.Name,
            Description = mt.Description,
            Price = mt?.Price,
            Images = mt.Images
                ?.Select(image => image.Url)
                .ToList()!,
            ParentId = mt?.ParentId,
            IsCategory = mt.IsCategory,
            Children = mt.Children != null && mt.Children.Any()
                ? MapToDto(mt.Children) // Recursively map children
                : new List<MealDto>()
        }).ToList();
    }

    public async Task<MealDto> SaveImagesOfMeal(int mealId, List<IFormFile> files)
    {
        var meal = await _context.Meals
            .Include(m => m.Children)
            .Include(m => m.Images)
            .FirstOrDefaultAsync(m => m.Id == mealId);

        if (meal is null || files is null)
            return null!;

        var fileNames = new List<string>();

        foreach(var file in files)
        {
          var fileName = await _fileService.SaveFileAsync(file, "meals");

            meal.Images.Add(new Image
            {
                Url = fileName,
                Description = file.FileName,
                MealId = mealId,
            });
        }

        _context.Meals.Update(meal);

        var isSaved = await _context.SaveChangesAsync() > 0;

        return isSaved
            ? new MealDto
            {
                Id = meal.Id,
                Name = meal.Name,
                Price = meal.Price,
                Description = meal.Description,
                ParentId = meal.ParentId,
                IsCategory = meal.IsCategory,
                Images = meal.Images.Select(x => x.Url).ToList(),
                Children = MapToDto(meal.Children)
            } : null!;
    }   

    public async Task<MealDto> DeleteImagesOfMeal(int mealId)
    {
        var meal = await _context.Meals
            .Include(m => m.Children)
            .Include(meal => meal.Images)
            .FirstOrDefaultAsync(x => x.Id == mealId);

        if (meal == null)
            return null!;

        var isDeleted = false;

        var images = new List<Image>();
        images.AddRange(meal.Images.ToList());

        foreach (var image in images)
        {
          isDeleted = await _fileService.DeleteFile(image.Url, "meals");

            if(isDeleted)
                meal.Images.Remove(image);
        }
        _context.Meals.Update(meal);
        isDeleted = await _context.SaveChangesAsync() > 0;

        return isDeleted
            ? new MealDto
            {
                Id = meal.Id,
                Name = meal.Name,
                Price = meal.Price,
                Description = meal?.Description,
                ParentId = meal?.ParentId,
                IsCategory = meal.IsCategory,
                Images = meal.Images.Select(x => x.Url).ToList(),
                Children = MapToDto(meal.Children)
            } : null!;
    }

    public Task<string> GetUrl(string fileName)
    {
        return _fileService.GetFileUrl(fileName, "meals");
    }

    public async Task<IList<MealDto>> GetCategories()
    {
        var categories = await _context.Meals
            .Where(m => m.IsCategory && m.Active && m.Name != "")
            .ToListAsync();

        return categories.Select(c => new MealDto
        {
            Id = c.Id,
            Name = c.Name,
            IsCategory = c.IsCategory,
            Children = MapToDto(c.Children),
            Description = c.Description
        }).ToList();
    }

    public async Task<IList<MealDto>> GetMealsByIds(List<int> Ids)
    {
        var meals = await _context.Meals
            .Include(m => m.Images)
            .Where(m => Ids.Any(x => x == m.Id))
            .ToListAsync();
        
        if(meals is null)
            return new List<MealDto>();

        return meals.Select(c => new MealDto
        {
            Id = c.Id,
            Name = c.Name,
            Price = c.Price,
            Images = c.Images.Select(i => i.Url).ToList(),
            IsCategory = c.IsCategory,
            Children = MapToDto(c.Children),
            Description = c.Description!
        }).ToList();
    }
}
