using Microsoft.EntityFrameworkCore;
using N5WebApi.Application.Abstractions;
using N5WebApi.Domain.Abstractions;

namespace N5WebApi.src.Infrastructure.Data;

internal class Repository<TEntity>(Context _context) : IRepository<TEntity> where TEntity : class, IEntity
{
    private readonly Context _dbContext = _context;

    public void Add(TEntity entity)
    {
        _dbContext.Set<TEntity>().Add(entity);
    }

    public IQueryable<TEntity> GetAll()
        => _dbContext.Set<TEntity>();

    public async Task<TEntity?> GetById<TKey>(TKey id)
        => await _dbContext.Set<TEntity>().FindAsync(id);

    public void Update(TEntity entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
    }
}