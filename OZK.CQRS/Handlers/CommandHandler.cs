using Microsoft.EntityFrameworkCore.Storage;
using OZK.Datalayer.Context;
using OZK.Datalayer.Contracts;
using OZK.Domain.CQRS.Contracts;
using OZK.Infrastructure.Events;
using System.Threading;
using System.Threading.Tasks;

namespace OZK.Domain.CQRS.Handlers
{
    public class CommandHandler : ICommandHandler
    {
        private readonly OZKDbContext _context;
     

        public CommandHandler(OZKDbContext context)
        {
            _context = context;
         
        }

        public Task<IDbContextTransaction> CreateTransaction(CancellationToken token) {
            return _context.Database.BeginTransactionAsync(token);
        }

        public async Task<TOut> ExecuteCommand<T, TOut>(ICustomCommand<T, TOut> customCommand, T model, CancellationToken token) where T : class
        {
            token.ThrowIfCancellationRequested();
            return await customCommand.Execute(_context, model, token);
        }
    }
}
