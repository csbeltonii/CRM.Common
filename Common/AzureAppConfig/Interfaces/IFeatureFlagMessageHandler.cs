using Microsoft.Azure.ServiceBus;

namespace Common.AzureAppConfig.Interfaces;

public interface IFeatureFlagMessageHandler
{

    bool HasChanges { get; }

    Task Handle(Message message, CancellationToken cancellationToken);

    Task OnException(ExceptionReceivedEventArgs exception);
}