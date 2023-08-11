using Common.AzureAppConfig.Interfaces;
using Common.AzureAppConfig.Services;
using Microsoft.Azure.ServiceBus;
using Moq;

namespace Common.Tests.AzureAppConfig.Services;

public class FeatureFlagChangeListenerShould
{
    private readonly Mock<ISubscriptionClient> _subscriptionClient;
    private readonly Mock<IFeatureFlagMessageHandler> _messageHandler;
    private readonly Mock<IFeatureFlagService> _featureFlagService;

    public FeatureFlagChangeListenerShould()
    {
        _subscriptionClient = new Mock<ISubscriptionClient>();
        _featureFlagService = new Mock<IFeatureFlagService>();
        _messageHandler = new Mock<IFeatureFlagMessageHandler>();
    }

    public class Start : FeatureFlagChangeListenerShould
    {
        [Fact]
        public async Task Successfully()
        {
            // arrange

            _subscriptionClient
                .Setup(mock => mock.RegisterMessageHandler(
                           It.IsAny<Func<Message, CancellationToken, Task>>(),
                           It.IsAny<Func<ExceptionReceivedEventArgs, Task>>()
                       )
                )
                .Verifiable();

            _featureFlagService
                .Setup(mock => mock.GetAllFeatureFlagSettings(It.IsAny<CancellationToken>()))
                .Verifiable();

            // act

            var sut = new FeatureFlagChangeListener(
                _subscriptionClient.Object,
                _messageHandler.Object,
                _featureFlagService.Object
            );

            await sut.StartAsync(CancellationToken.None);

            // assert
            _subscriptionClient.Verify();
            _featureFlagService.Verify();
        }
    }

    public class Stop : FeatureFlagChangeListenerShould
    {
        [Fact]
        public async Task Successfully()
        {
            // arrange
            _subscriptionClient
                .Setup(mock => mock.CloseAsync())
                .Verifiable();

            // act
            var sut = new FeatureFlagChangeListener(
                _subscriptionClient.Object,
                _messageHandler.Object,
                _featureFlagService.Object
            );

            await sut.StartAsync(CancellationToken.None);
            await sut.StopAsync(CancellationToken.None);

            // assert
            _subscriptionClient.Verify();
        }
    }
}