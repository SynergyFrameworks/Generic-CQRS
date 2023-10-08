using Microsoft.EntityFrameworkCore.Storage;
using OZK.Datalayer.Context;
using OZK.Datalayer.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace OZK.Domain.CQRS.Contracts
{
    public interface ICustomCommand<T, TOut>
    {
        Task<TOut> Execute(OZKDbContext context, T model, CancellationToken token = default);
    }
}
