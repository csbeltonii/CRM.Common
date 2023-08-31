using Common.Utility;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Common.ServiceCollectionExtensions;

public static class CosmosDbExtensions
{
    public static IServiceCollection AddCosmosDb(this IServiceCollection services, IConfiguration configuration)
    {
        var cosmosConnectionString = configuration["CosmosDbConnectionString"];

        var cosmosOptions = new CosmosClientOptions
        {
            ConnectionMode = ConnectionMode.Gateway,
            SerializerOptions = new CosmosSerializationOptions
            {
                IgnoreNullValues = true,
                PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase
            }
        };

        services
            .AddOptions()
            .Configure<DatabaseSettings>(configuration.GetSection("DatabaseSettings"));

        services
            .AddSingleton(new CosmosClient(cosmosConnectionString, cosmosOptions));

        return services;
    }
}

