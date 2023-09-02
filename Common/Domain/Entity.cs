﻿using Newtonsoft.Json;

namespace Common.Domain;

public abstract class Entity
{
    public string Id { get; set; }
    public string OrganizationId { get; set; }
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
}