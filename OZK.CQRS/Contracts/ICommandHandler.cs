using Microsoft.EntityFrameworkCore.Storage;
using OZK.Datalayer.Context;
using OZK.Datalayer.Contracts;
using OZK.Domain.CQRS.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace OZK.Domain.CQRS.Contracts
{
    public interface ICommandHandler
    {
        Task<IDbContextTransaction> CreateTransaction(CancellationToken token);

        Task<TOut> ExecuteCommand<T, TOut>(ICustomCommand<T, TOut> customCommand, T model, CancellationToken token) where T : class;
    }
}