using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.Models.BasketDTOs;
using Restaurant.Application.Services.BasketServices;

namespace Restaurant.API.Controllers.Baskets
{
    public class BasketController : BaseController
    {
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        [HttpGet]
        public async Task<BasketDto> GetBasket()
            => await _basketService.GetBasketAsync(UserDto);
        
        [HttpGet("GetBasketById/{id}")]
        public async Task<BasketDto> GetBasketById(int id)
            => await _basketService.GetBasketByIdAsync(id,UserDto);

        [HttpGet("GetBasketByUserId/{id}")]
        public async Task<BasketDto> GetBasketByUserId(int id)
            => await _basketService.GetBasketByUserIdAsync(id,UserDto);

        [HttpPost]
        public async Task<BasketDto> SaveBasket([FromBody] BasketDto basket)
            => await _basketService.SaveBasketAsync(basket, UserDto);

        [HttpPost("SetInActiveBasketItem")]
        public async Task<bool> SetInActiveBasketItem(int basketId)
        {
            return await _basketService.SetInActiveBasketItems(basketId);
        }
    }
}
