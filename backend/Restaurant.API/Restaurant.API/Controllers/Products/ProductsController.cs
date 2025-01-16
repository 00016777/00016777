using Microsoft.AspNetCore.Mvc;
using Restaurant.API.Attributes;
using Restaurant.Application.Models.ProductDTOs;
using Restaurant.Application.Services.ProductServices;
using Restaurant.Domain.Commons;
using Restaurant.Domain.Entities.Identity;

namespace Restaurant.API.Controllers.Products;

public class ProductsController : BaseController
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpPost("GetAllProducts")]
    public async Task<IList<ProductDto>> GetAllProducts([FromBody] Filter filter)
    {
        return await _productService.GetAllProducts(filter, UserDto);
    }

    [HttpGet("GetProductById/{productId}")]
    public async Task<ActionResult<ProductDto>> GetProductById(int productId)
    {
        return await _productService.GetProductById(productId, UserDto);
    }

    [HttpPost("CreateProduct"), AuthorizedRoles(Roles.Suplier)]
    public async Task<ActionResult<ProductDto>> CreateProduct([FromBody] ProductDto productDto)
    {
        return await _productService.CreateAsync(productDto, UserDto);
    }

    [HttpPost("UpdateProduct"), AuthorizedRoles(Roles.Suplier)]
    public async Task<ActionResult<ProductDto>> UpdateProduct([FromBody] ProductDto productDto)
    {
        return await _productService.UpdateAsync(productDto, UserDto);
    }

    [HttpDelete("DeleteProduct/{productId}"), AuthorizedRoles(Roles.Suplier)]
    public async Task<ActionResult<bool>> DeleteProduct(int productId)
    {
        return await _productService.DeleteAsync(productId, UserDto);
    }

    [HttpPost("UploadImagesOfProduct"), AuthorizedRoles(Roles.Suplier)]
    public async Task<ProductDto> UploadImagesOfProduct(int productId, List<IFormFile> files)
    {
        return await _productService.SaveImagesOfProduct(productId, files);
    }

    [HttpDelete("DeleteProductImages/{productId}")]
    public async Task<ProductDto> DeleteProductImages(int productId)
    {
        return await _productService.DeleteImagesOfProduct(productId);
    }

    [HttpPost("GetProductsByIds")]
    public async Task<IList<ProductDto>> GetProductsByIds(List<int> ids)
    {
        return await _productService.GetProductsByIds(ids);
    }
}
