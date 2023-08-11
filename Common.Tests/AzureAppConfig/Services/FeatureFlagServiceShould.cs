using Common.AzureAppConfig.Models;
using Common.AzureAppConfig.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.FeatureManagement;
using Moq;

namespace Common.Tests.AzureAppConfig.Services;

public class FeatureFlagServiceShould
{
    private readonly Mock<IFeatureManager> _featureManager;
    private readonly IConfigurationRoot _configuration;

    public FeatureFlagServiceShould()
    {
        _featureManager = new Mock<IFeatureManager>();
        _configuration = BuildConfiguration();
    }

    public class RetrieveFeatureFlags : FeatureFlagServiceShould
    {
        [Fact]
        public async Task Successfully()
        {
            // arrange
            var expected = GetSampleFeatureFlagSettings();

            _featureManager
                .Setup(mock => mock.GetFeatureNamesAsync())
                .Returns(GetSampleFeatureKeyNames)
                .Verifiable();

            var sut = new FeatureFlagService(_featureManager.Object, _configuration);

            // act
            var result = await sut.GetAllFeatureFlagSettings(CancellationToken.None);

            // assert
            Assert.True(result.IsSuccess);
            Assert.Equal(expected, result.Value);

            _featureManager.Verify();
        }

        [Fact]
        public async Task Unsuccessfully()
        {
            // arrange
            _featureManager
                .Setup(mock => mock.GetFeatureNamesAsync())
                .Throws<Exception>()
                .Verifiable();

            // act
            var sut = new FeatureFlagService(_featureManager.Object, _configuration);

            var result = await sut.GetAllFeatureFlagSettings(CancellationToken.None);

            // assert
            Assert.False(result.IsSuccess);
        }
    }

    public static ICollection<FeatureFlagSetting> GetSampleFeatureFlagSettings()
        => new List<FeatureFlagSetting>
        {
            new("key1", true),
            new("key2", false),
            new("key3", true)
        };

    public static async IAsyncEnumerable<string> GetSampleFeatureKeyNames()
    {
        yield return "key1";
        yield return "key2";
        yield return "key3";
        await Task.CompletedTask;
    }

    public static IConfigurationRoot BuildConfiguration()
    {
        var settings = new Dictionary<string, string>()
        {
            {"FeatureManagement:key1", "true"},
            {"FeatureManagement:key2", "false"},
            {"FeatureManagement:key3", "true"}
        };

        var configuration = new ConfigurationBuilder()
                            .AddInMemoryCollection(settings)
                            .Build();

        return configuration;
    }
}