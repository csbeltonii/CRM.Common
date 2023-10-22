using System.Linq.Expressions;
using System.Net;
using Common.Domain;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;

namespace Common.Repositories;

public class BaseRepository<TEntity> : IRepository<TEntity>
    where TEntity : Entity
{
    protected readonly Container _container;

    public BaseRepository(CosmosClient cosmosClient, string databaseName, string containerName)
    {
        _container = cosmosClient.GetContainer(databaseName, containerName);
    }

    public async Task<TEntity> Create(TEntity entity, CancellationToken cancellationToken) =>
        await _container
              .CreateItemAsync(entity, entity.PartitionKey, cancellationToken: cancellationToken)
              .ConfigureAwait(false);

    public async Task<TEntity> Get(string id, string partitionKey, CancellationToken cancellationToken)
    {
        var entity = await _container
              .ReadItemAsync<TEntity>(id, new PartitionKey(partitionKey), cancellationToken: cancellationToken)
              .ConfigureAwait(false);

        return entity.Resource;
    }
   
    public async Task<IEnumerable<TEntity>> Get(string searchCriteria, CancellationToken cancellationToken)
    {
        var results = new List<TEntity>();

        var iterator = _container
            .GetItemQueryIterator<TEntity>(new QueryDefinition(searchCriteria));

        while (iterator.HasMoreResults)
        {
            var result = await iterator.ReadNextAsync(cancellationToken)
                                       .ConfigureAwait(false);

            results.AddRange(result.ToList());
        }

        return results;
    }

    public async Task<IEnumerable<TEntity>> Get(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
    {
        var results = new List<TEntity>();

        var iterator = _container
                       .GetItemLinqQueryable<TEntity>()
                       .Where(predicate)
                       .ToFeedIterator();

        while (iterator.HasMoreResults)
        {
            results.AddRange(await iterator.ReadNextAsync(cancellationToken));
        }

        return results;
    }

    public async Task<IEnumerable<TEntityDto>> Get<TEntityDto>(
        Expression<Func<TEntity, bool>> predicate, 
        Expression<Func<TEntity, TEntityDto>> selector,
        CancellationToken cancellationToken)
    {
        var results = new List<TEntityDto>();

        var iterator = _container
                       .GetItemLinqQueryable<TEntity>()
                       .Where(predicate)
                       .Select(selector)
                       .ToFeedIterator();

        while (iterator.HasMoreResults)
        {
            results.AddRange(await iterator.ReadNextAsync(cancellationToken));
        }

        return results;
    }

    public async Task<TEntity> Update(TEntity entity, string etag, CancellationToken cancellationToken)
    {
        try
        {
            var options = new ItemRequestOptions
            {
                IfMatchEtag = etag
            };

            var response = await _container.UpsertItemAsync(entity, entity.PartitionKey, options, cancellationToken)
                                           .ConfigureAwait(false);

            return response.Resource;
        }
        catch (CosmosException ex)
            when (ex.StatusCode is HttpStatusCode.NotFound or HttpStatusCode.PreconditionFailed)
        {
            return default;
        }
    }

    public async Task<bool> Delete(string id, string partitionKey, CancellationToken cancellationToken)
    {
        try
        {
            await _container.DeleteItemAsync<TEntity>(id, new PartitionKey(partitionKey), cancellationToken: cancellationToken)
                            .ConfigureAwait(false);

            return true;
        }
        catch (CosmosException ex)
            when (ex.StatusCode is HttpStatusCode.NotFound)
        {
            return false;
        }
    }
}