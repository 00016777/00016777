using Restaurant.Domain.Entities.MealEntities;

namespace Restaurant.Application.Models.MealDTOs;

public class MealDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal? Price { get; set; }
    public string Description { get; set; }
    public int? ParentId { get; set; }

    public bool IsCategory { get; set; }
    public List<string>? Images { get; set; }
    public List<MealDto>? Children { get; set; }
}
