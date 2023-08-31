using Newtonsoft.Json;

namespace Common.Domain;

public class Organization
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Owner { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime LastUpdated { get; set; }

    [JsonProperty("_etag")]
    public string Etag { get; set; }
}