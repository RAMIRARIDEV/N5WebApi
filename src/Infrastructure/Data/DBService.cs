using N5WebApi.Application.Abstractions;
using N5WebApi.Domain.Abstractions;
using N5WebApi.Domain.Exceptions;

namespace N5WebApi.src.Infrastructure.Data;

public class DBService<TEntity> : IDBService<TEntity> where TEntity : class, IEntity
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<TEntity> _repository;

    public DBService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _repository = unitOfWork.GetRepository<TEntity>();
    }

    public virtual async Task<Result> CreateAsync(TEntity entity)
    {
        try
        {
            _repository.Add(entity);
            return await _unitOfWork.SaveChangesAsync<TEntity>();
        }
        catch (Exception)
        {
            return Result.Failure(SaveChangesErrors.SaveChangesError);
        }

    }
    public virtual async Task<Result> UpdateAsync(TEntity entity)
    {
        try
        {
            _repository.Update(entity);
            return await _unitOfWork.SaveChangesAsync<TEntity>();
        }
        catch (Exception)
        {
            return Result.Failure(SaveChangesErrors.SaveChangesError);
        }
    }

    public virtual async Task<TEntity> GetByIdAsync<TKey>(TKey id)
    {
        TEntity? entity = await _repository.GetById(id);
        if (entity is null)
            Result.Failure(GetFromDbErrors.GetFromDbError);

        return entity!;
    }

    public IQueryable<TEntity> GetAll()
        => _repository.GetAll();

}

