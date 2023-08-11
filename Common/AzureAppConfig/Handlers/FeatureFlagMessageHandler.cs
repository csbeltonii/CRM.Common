using System.Diagnostics;
using Azure.Messaging.EventGrid;
using Common.AzureAppConfig.Hubs;
using Common.AzureAppConfig.Interfaces;
using Common.AzureAppConfig.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration.Extensions;
using Microsoft.Extensions.Options;

namespace Common.AzureAppConfig.Handlers;

public abstract class FeatureFlagMessageHandler : IFeatureFlagMessageHandler
{
    protected readonly IConfigurationRefresher _configurationRefresher;
    protected readonly IFeatureFlagService _featureFlagService;
    protected readonly IHubContext<FeatureHub> _featureHubContext;
    protected readonly IConfiguration _configuration;

    protected readonly FeatureFlagChangeListenerSettings _settings;

    public bool HasChanges { get; protected set; }

    protected FeatureFlagMessageHandler(IConfigurationRefresher configurationRefresher, 
                                        IFeatureFlagService featureFlagService,
                                        IHubContext<FeatureHub> featureHubContext,
                                        IOptions<FeatureFlagChangeListenerSettings> settings,
                                        IConfiguration configuration)
    {
        _configurationRefresher = configurationRefresher;
        _featureFlagService = featureFlagService;
        _featureHubContext = featureHubContext;
        _configuration = configuration;
        _settings = settings.Value;
    }


    public async Task Handle(Message message, CancellationToken cancellationToken)
    {
        var eventGridEvent = EventGridEvent.Parse(BinaryData.FromBytes(message.Body));

        if (eventGridEvent.EventType != EventGridEvents.KeyValueModified)
            return;

        eventGridEvent.TryCreatePushNotification(out var pushNotification);

        _configurationRefresher.ProcessPushNotification(pushNotification, TimeSpan.FromMilliseconds(_settings.Delay));

        await Task.Delay(_settings.Delay, cancellationToken);

        await _configurationRefresher.TryRefreshAsync(cancellationToken);

        if (HasChanges)
        {
            await _featureFlagService.GetAllFeatureFlagSettings(cancellationToken);
            await _featureHubContext.Clients.All.SendAsync(
                "FeatureFlagUpdate",
                _featureFlagService.CurrentValues,
                cancellationToken: cancellationToken
            );

            HasChanges = false;
        }
    }

    public Task OnException(ExceptionReceivedEventArgs exception)
    {
        Debug.WriteLine(exception.Exception.Message);

        return Task.CompletedTask;
    }
}