namespace Restaurant.Application.Models.BasketDTOs;

public class BasketDto
{
    public int Id { get; set; }
    public DateTime LastUpdated { get; set; }
    public ICollection<BasketItemDto> BasketItems { get; set; }
}
