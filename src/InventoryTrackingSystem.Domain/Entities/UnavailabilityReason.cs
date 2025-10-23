using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace InventoryTrackingSystem.Entities;

public class UnavailabilityReason:FullAuditedEntity<Guid>
{
    public string Reason { get; set; } = string.Empty;// Uygunsuzluk açıklaması (ARIZA, BAKIM, Sahada.)
    // EF Core için protected constructor
    protected UnavailabilityReason()
    {
    }
    // Ana constructor
    public UnavailabilityReason(Guid id) : base(id)
    {
    }
    
    // Parametreli constructor
    public UnavailabilityReason(Guid id, string reason) : base(id)
    {
        Reason = reason;
    }
}