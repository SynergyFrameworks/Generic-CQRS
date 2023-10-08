using OZK.Datalayer.Context;
using OZK.Datalayer.Contracts;
using OZK.Domain.CQRS.Commands;
using OZK.Domain.CQRS.Contracts;
using OZK.Infrastructure.Events;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace OZK.Domain.CQRS.Handlers
{
    //TODO: Pass type of T
    public class AddEvent : IEvent
    {
        public int Version { get; set; }
        public DateTimeOffset TimeStamp { get; set; }
        public Guid Id { get; set; }
    }

    public class CrudHandler : ICrudHandler<OZKDbContext>
    {
        private OZKDbContext _context;
       

        public CrudHandler(OZKDbContext context)
        {
            _context = context;
            
        }

        public async Task<T> AddHandler<T>(T model, CancellationToken cancellationToken = default) where T : class, IEntity, IAuditable
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var transaction = await _context.Database.BeginTransactionAsync(cancellationToken))
            {
                await _context.InsertEntityAsync(model, cancellationToken);

                if (!cancellationToken.IsCancellationRequested)
                {
                    try
                    {
                        await _context.SaveChangesAsync(cancellationToken);
                        await transaction.CommitAsync(cancellationToken);
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }

                    model.State = Datalayer.Domain.ModelState.Added;
                    //TODO: Pass the version...
                    //_eventBus?.Commit(new AddEvent { Id = model.Id, TimeStamp = model.CreatedDate, Version = 1 });
                }
                else
                {
                    transaction.Rollback();
                }

                _context.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
                return model;
            }
        }

        public async Task<T> DeleteHandler<T>(T model, CancellationToken cancellationToken) where T : class, IEntity, IAuditable
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var transaction = await _context.Database.BeginTransactionAsync(cancellationToken))
            {
                await _context.DeleteEntityAsync(model, cancellationToken);
                if (!cancellationToken.IsCancellationRequested)
                {
                    try
                    {
                        await _context.SaveChangesAsync(cancellationToken);
                        await transaction.CommitAsync(cancellationToken);
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }

                    model.State = Datalayer.Domain.ModelState.Deleted;
                }
                else
                {
                    transaction.Rollback();
                }

                _context.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
                return model;
            }
        }

        public async Task<T> UpdateHandler<T>(T model, CancellationToken cancellationToken) where T : class, IEntity, IAuditable
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var transaction = await _context.Database.BeginTransactionAsync(cancellationToken))
            {
                await _context.UpdateEntityAsync(model, cancellationToken);

                if (!cancellationToken.IsCancellationRequested)
                {
                    try
                    {
                        await _context.SaveChangesAsync(cancellationToken);
                        await transaction.CommitAsync(cancellationToken);
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }

                    model.State = Datalayer.Domain.ModelState.Updated;
                }
                else
                {
                    transaction.Rollback();
                }

                _context.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
                return model;
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
