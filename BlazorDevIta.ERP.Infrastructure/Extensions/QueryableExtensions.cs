using BlazorDevIta.ERP.Infrastructure.Attributes;
using BlazorDevIta.ERP.Infrastructure.DataTypes;
using System.Linq.Expressions;
using System.Reflection;



namespace BlazorDevIta.ERP.Infrastructure.Extensions
{


    public static class QueryableExtensions
    {

        #region Private 


        private static readonly MethodInfo OrderByMethod = typeof(Queryable)
                  .GetMethods()
                  .Where(method => method.Name == nameof(Queryable.OrderBy))
                  .Where(method => method.GetParameters().Length == 2)
                  .Single();

        private static readonly MethodInfo OrderByDescendingMethod = typeof(Queryable)
                   .GetMethods()
                   .Where(method => method.Name == nameof(Queryable.OrderByDescending))
                   .Where(method => method.GetParameters().Length == 2)
                   .Single();


        private static object? GetMethodInvoke<TSource>(IQueryable<TSource> source, string propertyName, OrderDirection direction)
        {

            if (source == null) throw new ArgumentNullException(nameof(source));

            if (string.IsNullOrWhiteSpace(propertyName)) return null;

            var property = typeof(TSource).GetProperties()
                .Where(p => p.Name == propertyName)
                .Where(p => p.GetCustomAttribute<NotOrderableAttribute>() == null)
                .SingleOrDefault();

            if (property == null) return null;

            // x => x.<proprietà> (es. x => x.Date)

            // x

            ParameterExpression parameter = Expression.Parameter(typeof(TSource), "x");

            // x.<propertyName>
            Expression orderByProperty = Expression.Property(parameter, property);

            // x => x.<propertyName>
            LambdaExpression lambda = Expression.Lambda(orderByProperty, new[] { parameter });

            MethodInfo genericMethod = direction == OrderDirection.Ascendent ? OrderByMethod.MakeGenericMethod
                (new[] { typeof(TSource), orderByProperty.Type }) :
                OrderByDescendingMethod.MakeGenericMethod(new[] { typeof(TSource), orderByProperty.Type });


            object? ret = genericMethod.Invoke(null, new object[] { source, lambda });

            return ret;
        }


        #endregion


        #region Methods with Action


        public static IQueryable<TSource> OrderByPropertyAscending<TSource>(this IQueryable<TSource> source, string propertyName, Action reset)
        {
            return OrderByProperty(source, propertyName, OrderDirection.Ascendent, reset);
        }

        public static IQueryable<TSource> OrderByPropertyDescending<TSource>(this IQueryable<TSource> source, string propertyName, Action reset)
        {
            return OrderByProperty(source, propertyName, OrderDirection.Descendent, reset);
        }

        public static IQueryable<TSource> OrderByProperty<TSource>(this IQueryable<TSource> source, string propertyName, OrderDirection direction, Action reset)
        {
            object? ret = GetMethodInvoke(source, propertyName, direction);

            if (ret != null)
            {
                return (IQueryable<TSource>)ret;
            }
            else
            {
                reset?.Invoke();
                return source;
            }

        }


        #endregion


        #region Methods with wrapped object


        public static OrderedQueryable<TSource> GetOrderedValuesAscending<TSource>(this IQueryable<TSource> source, string propertyName)
        {
            return GetOrderedValues(source, propertyName, OrderDirection.Ascendent);
        }

        public static OrderedQueryable<TSource> GetOrderedValuesDescending<TSource>(this IQueryable<TSource> source, string propertyName)
        {
            return GetOrderedValues(source, propertyName, OrderDirection.Descendent);
        }

        public static OrderedQueryable<TSource> GetOrderedValues<TSource>(this IQueryable<TSource> source, string propertyName, OrderDirection direction)
        {
            object? ret = GetMethodInvoke(source, propertyName, direction);

            if (ret != null)
            {
                return new OrderedQueryable<TSource>(false, (IQueryable<TSource>)ret);
            }
            else
            {
                return new OrderedQueryable<TSource>(true, source);
            }

        }


        #endregion


        #region Methods with Try & out parameter


        public static bool TryOrderByPropertAscending<TSource>(this IQueryable<TSource> source, string propertyName, out IQueryable<TSource> queryable)
        {
            return TryOrderByProperty(source, propertyName, OrderDirection.Ascendent, out queryable);
        }

        public static bool TryOrderByPropertDescending<TSource>(this IQueryable<TSource> source, string propertyName, out IQueryable<TSource> queryable)
        {
            return TryOrderByProperty(source, propertyName, OrderDirection.Descendent, out queryable);
        }

        public static bool TryOrderByProperty<TSource>(this IQueryable<TSource> source, string propertyName, OrderDirection direction, out IQueryable<TSource> queryable)
        {

            object? ret = GetMethodInvoke(source, propertyName, direction);

            if (ret != null)
            {
                queryable = (IQueryable<TSource>)ret;

                return true;
            }
            else
            {
                queryable = null!;

                return false;
            }

           

        }


        #endregion


    }
}

