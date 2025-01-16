namespace Restaurant.Application.Models.OrderDTOs;

public class OrderDetailDto
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int? MealId { get; set; }
    public int? ProductId { get; set; }
    public int? DrinkId { get; set; }
    public int Quantity { get; set; }
}
