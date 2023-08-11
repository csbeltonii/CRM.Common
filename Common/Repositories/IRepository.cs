namespace Common.Repositories;

public interface IRepository<TEntity>
    where TEntity : class
{
    Task<TEntity> Create(TEntity entity, string partitionKey, CancellationToken cancellationToken);
    Task<TEntity?> Get(string id, string partitionKey, CancellationToken cancellationToken);
    Task<IEnumerable<TEntity>> Get(string searchCriteria, CancellationToken cancellationToken);
    Task<TEntity?> Update(TEntity entity, string etag, string partitionKey, CancellationToken cancellationToken);
    Task<bool> Delete(string id, string partitionKey, CancellationToken cancellationToken);
}