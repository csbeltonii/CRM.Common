using Newtonsoft.Json;

namespace Domain.Model;

public abstract class Entity
{
    public string Id { get; set; }
    public string OrganizationId { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime LastUpdated { get; set; }

    [JsonProperty("_etag")]
    public string Etag { get; set; }
}