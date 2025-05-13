namespace N5WebApi.Application.Abstractions;

public  interface IElasticService<T>
{
    bool CreateDocument(T entity);
}
