using System.Linq.Expressions;
using Common.Domain;

namespace Common.Repositories;

public interface IRepository<TEntity>
    where TEntity : Entity
{
    Task<TEntity> Create(TEntity entity, CancellationToken cancellationToken);

    Task<TEntity> Get(string id, string partitionKey, CancellationToken cancellationToken);

    Task<IEnumerable<TEntity>> Get(string searchCriteria, CancellationToken cancellationToken);

    Task<IEnumerable<TEntity>> Get(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);

    Task<IEnumerable<TEntityDto>> Get<TEntityDto>(Expression<Func<TEntity, bool>> predicate,
                                                  Expression<Func<TEntity, TEntityDto>> selector,
                                                  CancellationToken cancellationToken);

    Task<TEntity> Update(TEntity entity, string etag, CancellationToken cancellationToken);

    Task<bool> Delete(string id, string partitionKey, CancellationToken cancellationToken);
}