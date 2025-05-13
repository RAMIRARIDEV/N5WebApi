using N5WebApi.Domain.Abstractions;

namespace N5WebApi.Application.Abstractions;
public interface IRepository<TEntity> where TEntity : class, IEntity
{
    Task<TEntity?> GetById<TKey>(TKey id);
    void Add(TEntity entity);
    void Update(TEntity entity);
    IQueryable<TEntity> GetAll();
}