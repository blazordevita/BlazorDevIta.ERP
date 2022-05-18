using BlazorDevIta.ERP.Infrastructure.Attributes;
using BlazorDevIta.ERP.Infrastructure.DataTypes;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;


namespace BlazorDevIta.ERP.Infrastructure.Extensions;

public static class QueryableExtensions
{

    #region Public methods


    public static IOrderedQueryable<T> OrderByPropertyOrField<T>(this IQueryable<T> source, string propertyOrFieldName, OrderDirection direction = OrderDirection.Ascendent)
    {
        return GetOrderedQueryable(source, OrderLevel.OrderBy, propertyOrFieldName, direction);
    }


    public static IOrderedQueryable<T> OrderByPropertyOrFieldWithCheck<T>(this IQueryable<T> source, string propertyOrFieldName, OrderDirection direction = OrderDirection.Ascendent, string defaultProperty = "Id")
    {
        if (string.IsNullOrWhiteSpace(propertyOrFieldName))
        {
            propertyOrFieldName = defaultProperty;
        }

        PropertyInfo? property = GetOrderableProperty<T>(propertyOrFieldName);

        if (property == null) return (IOrderedQueryable<T>)source;

        return source.OrderByPropertyOrField(propertyOrFieldName, direction);
    }


    public static IOrderedQueryable<T> ThenByPropertyOrField<T>(this IOrderedQueryable<T> source, string propertyOrFieldName, OrderDirection direction = OrderDirection.Ascendent)
    {
        return GetOrderedQueryable(source, OrderLevel.ThenBy, propertyOrFieldName, direction);
    }

    public static IOrderedQueryable<T> ThenByPropertyOrFieldWithCheck<T>(this IOrderedQueryable<T> source, string propertyOrFieldName, OrderDirection direction = OrderDirection.Ascendent)
    {
        if (!string.IsNullOrWhiteSpace(propertyOrFieldName))
        {
            PropertyInfo? property = GetOrderableProperty<T>(propertyOrFieldName);

            if (property != null)
                return source.ThenByPropertyOrField<T>(propertyOrFieldName, direction);

        }

        return source;
    }


    #endregion


    #region Private methods


    private static IOrderedQueryable<T> GetOrderedQueryable<T>(IQueryable<T> source, OrderLevel level, string propertyOrFieldName, OrderDirection direction)
    {
        var elementType = typeof(T);

        string methodName = Enum.GetName(typeof(OrderLevel), level)!;
        var orderByMethodName = direction == OrderDirection.Ascendent ? methodName : $"{methodName}Descending";

        var parameterExpression = Expression.Parameter(elementType);
        var propertyOrFieldExpression = Expression.PropertyOrField(parameterExpression, propertyOrFieldName);
        var selector = Expression.Lambda(propertyOrFieldExpression, parameterExpression);


        var orderByExpression = Expression.Call(typeof(Queryable), orderByMethodName,
            new[] { elementType, propertyOrFieldExpression.Type }, source.Expression, selector);

        return (IOrderedQueryable<T>)source.Provider.CreateQuery<T>(orderByExpression);
    }

    private static PropertyInfo? GetOrderableProperty<T>(string propertyOrFieldName)
    {
        return typeof(T).GetProperties()
                    .SingleOrDefault(p => p.Name == propertyOrFieldName
                                       && p.GetCustomAttribute<NotOrderableAttribute>() == null);
    }


    #endregion


    #region Enums

    public enum OrderLevel
    {
        OrderBy,
        ThenBy
    }

    #endregion

}




