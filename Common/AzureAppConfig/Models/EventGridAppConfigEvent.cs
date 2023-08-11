using System.Text.Json.Serialization;

namespace Common.AzureAppConfig.Models;

public class EventGridAppConfigEvent
{
    [JsonPropertyName("key")]
    public string Key { get; set; }

    [JsonPropertyName("label")]
    public string Label { get; set; }

    [JsonPropertyName("etag")]
    public string Etag { get; set; }

    [JsonPropertyName("syncToken")]
    public string SyncToken { get; set; }

    public string TrimmedKey => Key.Split("/").Last();
}