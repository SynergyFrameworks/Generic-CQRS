using Microsoft.EntityFrameworkCore;
using OZK.Infrastructure.Paging;
using OZK.Infrastructure.Sorting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace OZK.Domain.CQRS.Queries
{
    public static class GenericSelectionsQuery
    {
        public static async Task<ICollection<TOutput>> GetEntitiesAsync<TEntity, TOutput>(this DbContext context, Expression<Func<TEntity, TOutput>> projection, Expression<Func<TEntity, bool>> whereExpression = null, IList<ISortingOption> sortOptions = null, IPaging pageInfo = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            var dbSet = context.Set<TEntity>();

            if (whereExpression == null) return await dbSet.Select(projection).ToListAsync(cancellationToken);

            var query = dbSet
                           .Where(whereExpression);

            //TODO: refactor... extract to a method or introduce a visitor for each action pagingVisitor and sortVisitor...
            if (sortOptions?.Count > 0)
            {
                foreach (var sortOption in sortOptions)
                {
                    query = OrderBy(query, sortOption);
                }
            }

            if (pageInfo != null)
            {
                return await query
                                .Select(projection)
                                .Skip((pageInfo.CurrentPage - 1) * pageInfo.PageCount)
                                .Take(pageInfo.PageCount)
                                .ToListAsync(cancellationToken);
            }

            return await query
                            .Select(projection)
                            .ToListAsync(cancellationToken);
        }

        private static IQueryable<TEntity> OrderBy<TEntity>(IQueryable<TEntity> source, ISortingOption option)
        {
            ParameterExpression parameter = Expression.Parameter(source.ElementType); //(Client c)
            MemberExpression property = Expression.Property(parameter, option.ColumnName); //c.ClientName
            LambdaExpression lambdaExpression = Expression.Lambda(property, parameter); // (Client c) => c.ClientName

            string methodName = !option.IsColumnOrderDesc ? "OrderBy" : "OrderByDescending";
            Type[] types = new Type[] { source.ElementType, property.Type }; //Client, String
            Expression methodCallExpression = Expression.Call(typeof(Queryable), methodName, types, source.Expression, Expression.Quote(lambdaExpression)); //OrderBy((Client c) => c.ClientName)

            return source.Provider.CreateQuery<TEntity>(methodCallExpression); //Where clause...
        }
    }
}