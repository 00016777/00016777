using Restaurant.Domain.Commons;
using System.Linq.Expressions;
using Nancy.Json;

namespace Restaurant.Domain.Extensions;

public static class QuerableExtensions
{
    public static IQueryable<T> ApplyOrdering<T>(
        this IQueryable<T> query,
        Filter filter,
        string defaultSortField,
        Dictionary<string, Expression<Func<T, object>>> columnsMap)
    {
        if (string.IsNullOrWhiteSpace(filter.SortField) || !columnsMap.ContainsKey(filter.SortField))
            return query.OrderByDescending(columnsMap[defaultSortField]);

        return filter.SortOrder == 1
            ? query.OrderBy(columnsMap[filter.SortField])
            : query.OrderByDescending(columnsMap[filter.SortField]);
    }

    public static IQueryable<T> ApplyFiltering<T>(
            this IQueryable<T> query,
            string queryObj,
            Dictionary<string, Expression<Func<T, bool>>> columnsMap)
    {
        if (string.IsNullOrWhiteSpace(queryObj) || !columnsMap.ContainsKey(queryObj))
            return query;

        return query.Where(columnsMap[queryObj]);
    }

    public static IQueryable<T> ApplyFiltering<T>(
            this IQueryable<T> query,
            Filter filter,
            Func<string, int, Dictionary<string, Expression<Func<T, bool>>>> functionToFilterExpression
        )
    {
        var serializer = new JavaScriptSerializer();
        dynamic itemFilter = serializer.Deserialize<object>(filter.Filters);
        foreach (var item in itemFilter)
        {
            var value = "";
            string key = item.Key;
            foreach (var item2 in item.Value)
            {
                value += item2.Value;
                break;
            }

            _ = int.TryParse(value, out var id);

            query = query.ApplyFiltering(key, functionToFilterExpression(value, id));
        }

        return query;
    }
}
