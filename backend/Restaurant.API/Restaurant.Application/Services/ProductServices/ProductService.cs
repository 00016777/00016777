using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Nancy.ViewEngines;
using Restaurant.Application.Models.Identities;
using Restaurant.Application.Models.ProductDTOs;
using Restaurant.Application.Services.FileServices;
using Restaurant.Domain.Commons;
using Restaurant.Domain.Entities.FilesEntities;
using Restaurant.Domain.Entities.Identity;
using Restaurant.Domain.Entities.Products;
using Restaurant.Domain.Extensions;
using Restaurant.Infrastructure.DbContexts;

namespace Restaurant.Application.Services.ProductServices;

public class ProductService : IProductService
{
    private readonly AppDbContext _context;
    private readonly IFileService _fileService;

    public ProductService(AppDbContext context, IFileService fileService)
    {
        _context = context;
        _fileService = fileService;
    }

    public async Task<ProductDto> CreateAsync(ProductDto dto, UserDto user)
    {
        if (dto == null) throw new ArgumentNullException(nameof(dto));

        var role = await _context.Roles
            .FirstOrDefaultAsync(x => x.Id == user.MainRoleId);

        if (role?.Name != Roles.Suplier)
            throw new Exception("Only Suplier can create products");

        var newProduct = new Product()
        {
            Name = dto.Name,
            Description = dto.Description,
            PricePerKG = dto.PricePerKG
        };

        _context.Products.Add(newProduct);
        var isSaved = await _context.SaveChangesAsync() > 0;

        return await Task.FromResult(isSaved ? new ProductDto
        {
            Id = newProduct.Id,
            Name = newProduct.Name,
            Description = newProduct.Description,
            PricePerKG = newProduct.PricePerKG
        } : null!);
    }

    public async Task<bool> DeleteAsync(int id, UserDto user)
    {
        if (id <= 0)
            return await Task.FromResult(false);

        var role = await _context.Roles
            .FirstOrDefaultAsync(x => x.Id == user.MainRoleId);

        if (role?.Name != Roles.Suplier)
            throw new Exception("Only Suplier can delete product");

        var product = await _context.Products
            .Include(m => m.Images)
            .FirstOrDefaultAsync(m => m.Id == id);

        foreach (var image in product.Images)
        {
            await _fileService.DeleteFile(image.Url, "products");
        }

        if (product == null)
            return true;

        product.Active = false;
        //_context.Products.Remove(product);

        var isDeleted = await _context.SaveChangesAsync() > 0;

        return isDeleted;
    }

    public async Task<ProductDto> DeleteImagesOfProduct(int productId)
    {
        var product = await _context.Products
            .Include(meal => meal.Images)
            .FirstOrDefaultAsync(x => x.Id == productId);

        if (product == null)
            return null!;

        var isDeleted = false;

        var images = new List<Image>();
        images.AddRange(product.Images.ToList());

        foreach (var image in images)
        {
            isDeleted = await _fileService.DeleteFile(image.Url, "products");

            if (isDeleted)
                product.Images.Remove(image);
        }
        _context.Products.Update(product);
        isDeleted = await _context.SaveChangesAsync() > 0;

        return isDeleted
            ? new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                PricePerKG = product.PricePerKG,
                Description = product?.Description,
                Images = product.Images.Select(x => x.Url).ToList(),
            } : null!;
    }

    public async Task<IList<ProductDto>> GetAllProducts(Filter filter, UserDto user)
    {
        var query = _context.Products
        .Where(m => m.Active)
        .Include(m => m.Images)
        .AsNoTracking();

        var expression = new ProductExpression();

        query = query.ApplyFiltering(filter, expression.Filter)
            .ApplyOrdering(filter, "Id", expression.Sort());

        query = query.Select(q => new Product()
        {
            Id = q.Id,
            Name = q.Name,
            PricePerKG = q.PricePerKG,
            Description = q.Description,
            Images = q.Images,
            Active = q.Active,
            CreatedDate = q.CreatedDate,
            UpdatedDate = q.UpdatedDate
        });

        var products = await query.ToListAsync();

        return products.Select(m => new ProductDto
        {
            Id = m.Id,
            Name = m.Name,
            PricePerKG = m.PricePerKG,
            Description = m.Description,
            Images = m.Images
                .Where(m => m.Active)
                .Select(im => im.Url)
                .ToList(),
        }).ToList();
    }

    public async Task<ProductDto> GetProductById(int id, UserDto user)
    {
        if (id == 0)
            throw new Exception("Invalid product Id");

        var product = await _context.Products
            .Include(m => m.Images)
            .FirstOrDefaultAsync(m => m.Id == id && m.Active);

        if (product == null)
            throw new Exception("Product not found");

        return new ProductDto
        {
            Id = id,
            Name = product.Name,
            PricePerKG = product.PricePerKG,
            Description = product.Description!,
            Images = product.Images
                .Where(m => m.Active)
                .Select(im => im.Url).ToList(),
        };
    }

    public async Task<IList<ProductDto>> GetProductsByIds(List<int> ids)
    {
        var products = await _context.Products
            .Include(p => p.Images)
            .Where(p => ids.Any(x => x == p.Id) && p.Active)
            .ToListAsync();
        
        if(products == null)
            return new List<ProductDto>();

        return products.Select(p => new ProductDto
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            PricePerKG = p.PricePerKG,
            Images = p.Images
                    .Select(i => i.Url)
                    .ToList()
        }).ToList();
    }

    public async Task<ProductDto> SaveImagesOfProduct(int productId, List<IFormFile> files)
    {
        var product = await _context.Products
            .Include(m => m.Images)
            .FirstOrDefaultAsync(m => m.Id == productId);

        if (product is null || files is null)
            return null!;

        var fileNames = new List<string>();

        foreach (var file in files)
        {
            var fileName = await _fileService.SaveFileAsync(file, "products");

            product.Images.Add(new Image
            {
                Url = fileName,
                Description = file.FileName,
                ProductId = productId,
            });
        }

        _context.Products.Update(product);

        var isSaved = await _context.SaveChangesAsync() > 0;

        return isSaved
            ? new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                PricePerKG = product.PricePerKG,
                Description = product.Description,
                Images = product.Images.Select(x => x.Url).ToList(),
            } : null!;
    }

    public async Task<ProductDto> UpdateAsync(ProductDto dto, UserDto user)
    {
        if (dto == null) throw new ArgumentNullException(nameof(dto));

        var role = await _context.Roles
            .FirstOrDefaultAsync(x => x.Id == user.MainRoleId);

        if (role?.Name != Roles.Suplier)
            throw new Exception("Only Product can create products");

        var existinMeal = await _context.Products
            .Include(m => m.Images)
            .FirstOrDefaultAsync(x => x.Active && x.Id == dto.Id);

        if (existinMeal == null)
            throw new Exception("Product not found !");

        existinMeal.Name = dto.Name;
        existinMeal.Description = dto.Description;
        existinMeal.PricePerKG = dto.PricePerKG;

        _context.Products.Update(existinMeal);
        var isSaved = await _context.SaveChangesAsync() > 0;

        return isSaved
            ? new ProductDto()
            {
                Id = existinMeal.Id,
                Name = existinMeal.Name,
                Description = existinMeal.Description,
                PricePerKG = existinMeal.PricePerKG,
                Images = existinMeal.Images
                .Where(m => m.Active)
                .Select(im => im.Url).ToList(),
            }
            : throw new Exception("Updating Product failed");
    }
}
