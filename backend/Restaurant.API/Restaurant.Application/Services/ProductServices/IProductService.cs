using Microsoft.AspNetCore.Http;
using Restaurant.Application.Models.Identities;
using Restaurant.Application.Models.ProductDTOs;
using Restaurant.Domain.Commons;

namespace Restaurant.Application.Services.ProductServices;

public interface IProductService
{
    Task<ProductDto> CreateAsync(ProductDto dto, UserDto user);
    Task<ProductDto> UpdateAsync(ProductDto dto, UserDto user);
    Task<bool> DeleteAsync(int id, UserDto user);
    Task<ProductDto> GetProductById(int id, UserDto user);
    Task<IList<ProductDto>> GetAllProducts(Filter filter, UserDto user);
    Task<ProductDto> SaveImagesOfProduct(int productId, List<IFormFile> files);
    Task<ProductDto> DeleteImagesOfProduct(int productId);
    Task<IList<ProductDto>> GetProductsByIds(List<int> ids);
}
