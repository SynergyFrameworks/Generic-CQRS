using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace OZK.Domain.CQRS.Queries
{
    public static class GenericCountQuery
    {
        public static async Task<int> GetCountAsync<TEntity>(this DbContext context, Expression<Func<TEntity, bool>> whereExpression = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            var dbSet = context.Set<TEntity>();
            if (whereExpression == null) return await dbSet.CountAsync(cancellationToken);

            return await dbSet
                 .Where(whereExpression)
                 .CountAsync(cancellationToken);
        }
    }
}