using Restaurant.Application.Expressions;
using Restaurant.Domain.Entities.MealEntities;
using Restaurant.Domain.Entities.Products;
using System.Linq.Expressions;

namespace Restaurant.Application.Services.ProductServices;

public class ProductExpression : CrudExpression<Product>
{
    public override Dictionary<string, Expression<Func<Product, bool>>> Filter(string strFilter, int intFilter)
    {
        return new Dictionary<string, Expression<Func<Product, bool>>>
        {
            {nameof(Product.Id), f => f.Id == intFilter},
            {"name", f => f.Name.ToLower().Contains(strFilter.ToLower())},
            {nameof(Product.PricePerKG) , f => f.PricePerKG == intFilter},
        };
    }

    public override Dictionary<string, Expression<Func<Product, object>>> Sort()
    {
        return new Dictionary<string, Expression<Func<Product, object>>>
        {
            {nameof(Product.Id), s => s.Id },
            {nameof(Product.Name), s => s.Name },
            {nameof(Product.PricePerKG), s => s.PricePerKG }
        };
    }
}
