using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BlazorDevIta.ERP.Infrastructure.Extensions
{
    public static class QueryableExtensions
    {
        private static readonly MethodInfo OrderByMethod = typeof(Queryable)
                   .GetMethods()
                   .Where(method => method.Name == "OrderBy")
                   .Where(method => method.GetParameters().Length == 2)
                   .Single();

        private static readonly MethodInfo OrderByDescendingMethod = typeof(Queryable)
           .GetMethods()
           .Where(method => method.Name == "OrderByDescending")
           .Where(method => method.GetParameters().Length == 2)
           .Single();

        public static IQueryable<TSource> OrderByProperty<TSource>(this IQueryable<TSource> source, string propertyName)
        {
            // x => x.<proprietà> (es. x => x.Date)

            // x
            ParameterExpression parameter = Expression.Parameter(typeof(TSource), "x");
            
            // x.<propertyName>
            Expression orderByProperty = Expression.Property(parameter, propertyName);
            
            // x => x.<propertyName>
            LambdaExpression lambda = Expression.Lambda(orderByProperty, new[] { parameter });

            MethodInfo genericMethod = OrderByMethod.MakeGenericMethod
                (new[] { typeof(TSource), orderByProperty.Type });

            object? ret = genericMethod.Invoke(null, new object[] { source, lambda });
            return ret != null ? (IQueryable<TSource>)ret : throw new Exception("invoke failed");
        }

        public static IQueryable<TSource> OrderByPropertyDescending<TSource>(this IQueryable<TSource> source, string propertyName)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(TSource), "x");
            Expression orderByProperty = Expression.Property(parameter, propertyName);

            LambdaExpression lambda = Expression.Lambda(orderByProperty, new[] { parameter });
            MethodInfo genericMethod = OrderByDescendingMethod.MakeGenericMethod
                (new[] { typeof(TSource), orderByProperty.Type });
            object? ret = genericMethod.Invoke(null, new object[] { source, lambda });
            return ret != null ? (IQueryable<TSource>)ret : throw new Exception("invoke failed");
        }
    }
}

