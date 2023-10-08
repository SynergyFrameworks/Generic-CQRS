using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace OZK.Domain.CQRS.Queries
{
    public static class GenericSelectionQuery
    {
        public static async Task<TOutput> GetEntityAsync<TEntity, TOutput>(this DbContext context, Expression<Func<TEntity, TOutput>> projection, Expression<Func<TEntity, bool>> whereExpression, CancellationToken cancellationToken = default) where TEntity : class
        {
            var dbSet = context.Set<TEntity>();
            
            return await dbSet
                .Where(whereExpression)
                .Select(projection)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}