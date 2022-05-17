using System.Linq.Expressions;

 namespace BlazorDevIta.ERP.Infrastructure.DataTypes;

public static class QueryableExtension
{
    public static IQueryable<T> OrderByPropertyOrField<T>(this IQueryable<T> source, string propertyOrFieldName, OrderDirection direction = OrderDirection.Ascendent)
    {
        var elementType = typeof(T);
        var orderByMethodName = direction == OrderDirection.Ascendent ? "OrderBy" : "OrderByDescending";

        var parameterExpression = Expression.Parameter(elementType);
        var propertyOrFieldExpression = Expression.PropertyOrField(parameterExpression, propertyOrFieldName);
        var selector = Expression.Lambda(propertyOrFieldExpression, parameterExpression);

        var orderByExpression = Expression.Call(typeof(Queryable), orderByMethodName,
            new[] { elementType, propertyOrFieldExpression.Type }, source.Expression, selector);

        return source.Provider.CreateQuery<T>(orderByExpression);
    }

   
}
