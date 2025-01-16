using Restaurant.Application.Expressions;
using Restaurant.Domain.Entities.MealEntities;
using System.Linq.Expressions;

namespace Restaurant.Application.Services.MealServices;

public class MealExpression : CrudExpression<Meal>
{
    public override Dictionary<string, Expression<Func<Meal, bool>>> Filter(string strFilter, int intFilter)
    {
        return new Dictionary<string, Expression<Func<Meal, bool>>>
        {
            {nameof(Meal.Id), f => f.Id == intFilter},
            {nameof(Meal.Name), f => f.Name == strFilter},
            {nameof(Meal.Price) , f => f.Price == intFilter},
        };
    }

    public override Dictionary<string, Expression<Func<Meal, object>>> Sort()
    {
        return new Dictionary<string, Expression<Func<Meal, object>>>
        {
            {nameof(Meal.Id), s => s.Id },
            {nameof(Meal.Name), s => s.Name },
            {nameof(Meal.Price), s => s.Price }
        };
    }
}