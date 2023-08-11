using Common.AzureAppConfig.Interfaces;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Hosting;

namespace Common.AzureAppConfig.Services;

public class FeatureFlagChangeListener : IHostedService
{
    private readonly ISubscriptionClient _subscriptionClient;
    private readonly IFeatureFlagMessageHandler _messageHandler;
    private readonly IFeatureFlagService _featureFlagService;

    public FeatureFlagChangeListener(ISubscriptionClient subscriptionClient,
                                     IFeatureFlagMessageHandler messageHandler,
                                     IFeatureFlagService featureFlagService)
    {
        _subscriptionClient = subscriptionClient;
        _messageHandler = messageHandler;
        _featureFlagService = featureFlagService;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await _featureFlagService.GetAllFeatureFlagSettings(cancellationToken);

        _subscriptionClient.RegisterMessageHandler(_messageHandler.Handle, _messageHandler.OnException);

        await Task.CompletedTask;
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await _subscriptionClient.CloseAsync();
    }
}