using N5WebApi.Domain.Abstractions;

namespace N5WebApi.Application.Abstractions;

internal interface IDBService<TEntity> where TEntity : class, IEntity
{
    IQueryable<TEntity> GetAll();
    Task<TEntity> GetByIdAsync<TKey>(TKey id);
    Task<Result> CreateAsync(TEntity entity);
    Task<Result> UpdateAsync(TEntity entity);
}
