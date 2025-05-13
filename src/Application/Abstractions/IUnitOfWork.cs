using N5WebApi.Domain.Abstractions;

namespace N5WebApi.Application.Abstractions;
public interface IUnitOfWork
{
    Task BeginTransaction();
    Task CommitTransaction();
    Task RollBackTransaction();
    Task<Result> SaveChangesAsync<TEntity>() where TEntity : class, IEntity;
    IRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IEntity;
}