using Elastic.Clients.Elasticsearch;
using N5WebApi.Application.Abstractions;

namespace N5WebApi.src.Infrastructure.Data;

internal sealed class ElasticService<T>(ElasticsearchClient elasticsearchClient) : IElasticService<T> where T : class
{
    private readonly ElasticsearchClient _elasticsearchClient = elasticsearchClient;
    public bool CreateDocument(T entity)
    {
        IndexResponse? result =  _elasticsearchClient.Index(entity, idx => idx.Index(nameof(T)));

        return result.IsValidResponse;
    }
}