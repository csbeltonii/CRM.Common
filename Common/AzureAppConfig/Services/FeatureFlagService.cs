using Common.AzureAppConfig.Interfaces;
using Common.AzureAppConfig.Models;
using Common.Utility;
using Microsoft.Extensions.Configuration;
using Microsoft.FeatureManagement;

namespace Common.AzureAppConfig.Services;

public class FeatureFlagService : IFeatureFlagService
{
    private readonly IFeatureManager _featureManager;
    private readonly IConfiguration _configuration;

    private readonly ICollection<FeatureFlagSetting> _featureFlagSettings;

    public FeatureFlagService(IFeatureManager featureManager,
                              IConfiguration configuration)
    {
        _featureManager = featureManager;
        _featureFlagSettings = new List<FeatureFlagSetting>();
        _configuration = configuration;
    }

    public IEnumerable<FeatureFlagSetting> CurrentValues => _featureFlagSettings.AsEnumerable();

    public async Task<Result<IEnumerable<FeatureFlagSetting>>> GetAllFeatureFlagSettings(CancellationToken cancellationToken)
    {
        try
        {
            _featureFlagSettings.Clear();

            var keyNames = _featureManager.GetFeatureNamesAsync();

            await foreach (var keyName in keyNames.WithCancellation(cancellationToken))
            {
                var unused = bool.TryParse(_configuration[$"FeatureManagement:{keyName}"], out var isEnabled);

                _featureFlagSettings.Add(new FeatureFlagSetting(keyName, isEnabled));
            }

            return Result.Success(_featureFlagSettings.AsEnumerable());
        }
        catch (Exception ex)
        {
            return Result.Failure<IEnumerable<FeatureFlagSetting>>(ex.Message);
        }
    }
}