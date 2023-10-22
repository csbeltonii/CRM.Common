using Common.Interfaces;
using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;

namespace Common.Domain;

public abstract class Entity : IAudit
{
    public string Id { get; set; }
    public DateTime CreatedDate { get; set;  }
    public string CreatedBy { get; }
    public DateTime LastUpdated { get; set; }
    public string UpdatedBy { get; set; }

    [JsonProperty("_etag")]
    public string Etag { get; set; }

    protected Entity(string userId)
    {
        CreatedBy = userId;
    }

    public PartitionKey PartitionKey => new(Id);
}