using Newtonsoft.Json;

namespace Common.Domain;

public abstract class Entity
{
    public string Id { get; set; }
    public string OrganizationId { get; set; }
    public DateTime CreatedDate { get; }
    public string CreatedBy { get; }
    public DateTime LastUpdated { get; private set; }
    public string UpdatedBy { get; private set; }

    [JsonProperty("_etag")]
    public string Etag { get; set; }

    protected Entity(string userId)
    {
        CreatedBy = userId;
        CreatedDate = DateTime.UtcNow;
    }

    public void RefreshInfo(string userId)
    {
        LastUpdated = DateTime.UtcNow;
        UpdatedBy = userId;
    }
}