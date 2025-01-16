using Microsoft.AspNetCore.Http;
using Restaurant.Application.Models.Identities;
using Restaurant.Application.Models.MealDTOs;
using Restaurant.Domain.Commons;

namespace Restaurant.Application.Services.MealServices;

public interface IMealService
{
    Task<MealDto> CreateMealAsync(MealDto mealDto, UserDto user);
    Task<MealDto> UpdateMealAsync(MealDto mealDto, UserDto user);
    Task<bool> DeleteMealAsync(int id, UserDto user);
    Task<MealDto> GetMealByIdAsync(int id, UserDto user);
    Task<IList<MealDto>> GetAllMealsAsync(Filter filterDto, UserDto user);
    Task<MealDtos> GetMealsAndChildrenAsync(int? parentId, string search);
    Task<MealDto> SaveImagesOfMeal(int mealId, List<IFormFile> files);
    Task<MealDto> DeleteImagesOfMeal(int mealId);
    Task<string> GetUrl(string fileName);
    Task<IList<MealDto>> GetCategories();

    Task<IList<MealDto>> GetMealsByIds(List<int> Ids);
}
