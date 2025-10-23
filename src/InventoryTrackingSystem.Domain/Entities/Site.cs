using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace InventoryTrackingSystem.Entities;

public class Site:FullAuditedEntity<Guid>
{
    public string Type { get; set; } = string.Empty;// Location type (WAREHOUSE, FIELD, VEHICLE)
    public string? Address { get; set; }// Physical address of the location (optional)
    protected Site()
    {
    }
    // Main constructor
    public Site(Guid id) : base(id)
    {
    }
    // Constructor with parameters
    public Site(Guid id, string type, string? address = null) : base(id)
    {
        Type = type;
        Address = address;
    }
}