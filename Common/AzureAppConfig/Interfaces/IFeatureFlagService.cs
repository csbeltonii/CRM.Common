using Common.AzureAppConfig.Models;
using Common.Utility;

namespace Common.AzureAppConfig.Interfaces
{
    public interface IFeatureFlagService
    {
        public IEnumerable<FeatureFlagSetting> CurrentValues { get; }
        Task<Result<IEnumerable<FeatureFlagSetting>>> GetAllFeatureFlagSettings(CancellationToken cancellationToken);
    }
}