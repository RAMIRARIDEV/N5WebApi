using N5WebApi.Application.Abstractions;
using N5WebApi.Domain.Abstractions;
using N5WebApi.Domain.Exceptions;

namespace N5WebApi.src.Infrastructure.Data;

internal class UnitOfWork(Context _context) : IUnitOfWork
{
    public async Task BeginTransaction()
    {
        await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransaction()
    {
        if (_context.Database.CurrentTransaction != null)
            await _context.Database.CommitTransactionAsync();
    }

    public async Task RollBackTransaction()
    {
        if (_context.Database.CurrentTransaction != null)
            await _context.Database.RollbackTransactionAsync();
    }
    public async Task<Result> SaveChangesAsync<TEntity>() where TEntity : class, IEntity
    {
        try
        {
            if (await GetDbContext<TEntity>().SaveChangesAsync() > 0)
            {

                return Result.Success();
            }

            return Result.Failure(SaveChangesErrors.SaveChangesError);

        }
        catch (Exception)
        {
            return Result.Failure(SaveChangesErrors.SaveChangesError);
        }

    }

    public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IEntity
        => new Repository<TEntity>(GetDbContext<TEntity>());

    public Context GetDbContext<TEntity>() where TEntity : class, IEntity => _context;
}
