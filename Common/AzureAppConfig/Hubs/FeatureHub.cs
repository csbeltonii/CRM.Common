using Common.AzureAppConfig.Models;
using Microsoft.AspNetCore.SignalR;

namespace Common.AzureAppConfig.Hubs
{
    public class FeatureHub : Hub
    {
        public async Task SendMessage(EventGridAppConfigEvent featureSwitchEvent)
        {
            await Clients.All.SendAsync("FeatureFlagUpdate", featureSwitchEvent);
        }
    }
}