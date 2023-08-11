using Microsoft.Azure.Cosmos;

namespace Common.Factories.QueryDefinitionBuilder.v1;

public interface IQueryDefinitionBuilder<T>
{
    QueryDefinition Build();
}