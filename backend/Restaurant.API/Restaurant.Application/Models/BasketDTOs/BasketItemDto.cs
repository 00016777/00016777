using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant.Application.Models.BasketDTOs;

public class BasketItemDto
{
    public int Id { get; set; }
    public int BasketId { get; set; }
    public int? MealId { get; set; }
    public int? DrinkId { get; set; }
    public int? ProductId { get; set; }
    public int Quantity { get; set; }

    [NotMapped]
    public bool changed { get; set; }
}
