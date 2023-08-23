using Newtonsoft.Json;

namespace Common.Domain;

public class Organization
{
    public string Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime LastUpdated { get; set; }

    [JsonProperty("_etag")]
    public string Etag { get; set; }
}