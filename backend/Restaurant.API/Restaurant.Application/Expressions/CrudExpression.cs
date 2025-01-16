using Restaurant.Domain.Commons;
using System.Linq.Expressions;

namespace Restaurant.Application.Expressions;

public abstract class CrudExpression<T> where T : EntityBase
{
    public abstract Dictionary<string, Expression<Func<T, bool>>> Filter(string strFilter, int intFilter);

    public abstract Dictionary<string, Expression<Func<T, object>>> Sort();
}
