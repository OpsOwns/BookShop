using Serilog;
using Shop.Shared.Domain.Event;
using Shop.Shared.SeedWork;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Entity = Shop.Shared.Domain.Entity;

namespace Shop.Store.Infrastructure.Db
{
    public class BookUnitOfWork : IUnitOfWork
    {
        private readonly BookContext _bookContext;
        private readonly IDomainEventDispatcher _domainEventDispatcher;
        private readonly ILogger _logger;
        public BookUnitOfWork(BookContext bookContext, IDomainEventDispatcher domainEventDispatcher, ILogger logger)
        {
            _bookContext = bookContext;
            _domainEventDispatcher = domainEventDispatcher;
            _logger = logger;
        }
        public async Task<Result> Commit(CancellationToken token = default)
        {
            await using var dbContextTransaction = await _bookContext.Database.BeginTransactionAsync(token);
            try
            {
                var entities = _bookContext.ChangeTracker.Entries().Where(x => x.Entity is Entity)
                    .Select(x => (Entity)x.Entity).ToList();
                if (entities.Count is 0)
                    return Result.Failure<Result>("No entities to save");
                await _bookContext.SaveChangesAsync(token);
                entities.ForEach(async x => await _domainEventDispatcher.Dispatch(x.DomainEvents.ToArray()));
                await dbContextTransaction.CommitAsync(token);
            }
            catch (Exception ex)
            {
                await dbContextTransaction.RollbackAsync(token);
                _logger.Error(ex.Message);
                _logger.Error($"Rollback transaction {DateTime.Now.TimeOfDay}");
                throw;
            }
            return Result.Success();
        }
    }
}
