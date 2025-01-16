using Microsoft.AspNetCore.Mvc;
using Restaurant.API.Attributes;
using Restaurant.Application.Models.MealDTOs;
using Restaurant.Application.Services.MealServices;
using Restaurant.Domain.Commons;
using Restaurant.Domain.Entities.Identity;

namespace Restaurant.API.Controllers.Meals;

public class MealController : BaseController
{
    private readonly IMealService _mealService;

    public MealController(IMealService mealService)
    {
        _mealService = mealService;
    }

    [HttpPost("GetAllMeals")]
    public async Task<IList<MealDto>> GetAllMeals([FromBody] Filter filter)
    {
        return await _mealService.GetAllMealsAsync(filter, UserDto);
    }

    [HttpGet("GetMealById/{mealId}")]
    public async Task<ActionResult<MealDto>> GetMealById(int mealId)
    {
        return await _mealService.GetMealByIdAsync(mealId, UserDto);
    }

    [HttpPost("CreateMeal"), AuthorizedRoles(Roles.Manager)]
    public async Task<ActionResult<MealDto>> CreateMeal([FromBody] MealDto mealDto)
    {
        return await _mealService.CreateMealAsync(mealDto, UserDto);
    }

    [HttpPost("UpdateMeal"), AuthorizedRoles(Roles.Manager)]
    public async Task<ActionResult<MealDto>> UpdateMeal([FromBody] MealDto mealDto)
    {
        return await _mealService.UpdateMealAsync(mealDto, UserDto);
    }

    [HttpDelete("DeleteMeal/{mealId}"), AuthorizedRoles(Roles.Manager)]
    public async Task<ActionResult<bool>> DeleteMeal(int mealId)
    {
        return await _mealService.DeleteMealAsync(mealId, UserDto);
    }

    [HttpGet("GetMealAndChildren")]
    public async Task<MealDtos> GetMealAndChildren(int parentId, string search)
    {
        return await _mealService.GetMealsAndChildrenAsync(parentId, search);
    }

    [HttpGet("GetImage{fileName}")]
    public async Task<string> GetImageUrl(string fileName)
    {
        return await _mealService.GetUrl(fileName);
    }

    [HttpPost, AuthorizedRoles(Roles.Manager)]
    public async Task<MealDto> UploadImagesOfMeal(int mealId, List<IFormFile> files)
    {
       return await _mealService.SaveImagesOfMeal(mealId, files);
    }

    [HttpDelete("DeleteMealImages/{mealId}")]
    public async Task<MealDto> DeleteMealImages(int mealId)
    {
        return await _mealService.DeleteImagesOfMeal(mealId);
    }

    [HttpGet("GetCategories")]
    public async Task<IList<MealDto>> GetCategories()
    {
        return await _mealService.GetCategories();
    }

    [HttpPost("GetMealsByIds")]
    public async Task<IList<MealDto>> GetMealsByIds([FromBody]List<int> mealIds)
    {
        return await _mealService.GetMealsByIds(mealIds);
    }

}
