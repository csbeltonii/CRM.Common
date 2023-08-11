using Azure.Messaging.ServiceBus.Administration;
using Common.AzureAppConfig.Interfaces;
using Common.AzureAppConfig.Models;
using Common.AzureAppConfig.Services;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FeatureManagement;

namespace Common.AzureAppConfig.Extensions
{
    public static class AzureAppConfigExtensions
    {
        public static async Task AddDynamicAzureAppConfigFeatureFlags(this IServiceCollection services, ConfigurationManager configuration)
        {
            await CreateAndAddServiceBusClient(services, configuration);

            services
                .Configure<FeatureFlagChangeListenerSettings>(featureFlagListenerSettings => configuration.GetSection("FeatureFlagChangeListenerOptions").Bind(featureFlagListenerSettings));

            configuration
                .AddAzureAppConfiguration(
                    options =>
                    {
                        options.Connect(configuration["ConnectionStrings:AzureAppConfig"])
                               .UseFeatureFlags(
                                   featureFlagOptions =>
                                   {
                                       featureFlagOptions.Select(KeyFilter.Any);
                                       featureFlagOptions.CacheExpirationInterval = TimeSpan.FromDays(1);
                                   });

                        var configurationRefresher = options.GetRefresher();
                        services.AddSingleton(configurationRefresher);
                    });

            services
                   .AddAzureAppConfiguration()
                   .AddFeatureManagement();

            services
                .AddSingleton<IFeatureFlagService, FeatureFlagService>()
                .AddHostedService<FeatureFlagChangeListener>();
        }

        private static async Task CreateAndAddServiceBusClient(IServiceCollection services, ConfigurationManager configuration)
        {
            var administrationClient = new ServiceBusAdministrationClient(configuration["ServiceBusSettings:ConnectionString"]);

            var subscriptionName = $"ff-sub-{Guid.NewGuid()}";

            var createSubscriptionOptions = new CreateSubscriptionOptions(
                configuration["ServiceBusSettings:TopicName"],
                subscriptionName
            )
            {
                AutoDeleteOnIdle = TimeSpan.FromDays(1),
                DefaultMessageTimeToLive = TimeSpan.FromDays(1)
            };

            var response = await administrationClient.CreateSubscriptionAsync(createSubscriptionOptions);

            services.AddSingleton<ISubscriptionClient>(
                new SubscriptionClient(
                    configuration["ServiceBusSettings:ConnectionString"],
                    response.Value.TopicName,
                    response.Value.SubscriptionName
                )
            );
        }
    }
}
