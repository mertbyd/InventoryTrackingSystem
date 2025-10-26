using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace InventoryTrackingSystem.Entities;

public class Inventory:FullAuditedEntity<Guid>
{
    public Guid SerialNumberId { get; set; }// Seri numarası ön eki (Grup adı: A, B, C)
    public string Name { get; set; } = string.Empty;// Envanter adı (Kürek, Levha, Kazma)
    public Guid CurrentSiteId { get; set; }// Envanterin şu an bulunduğu lokasyon
    public bool IsAvailableForRequest { get; set; } = true;// Yeni talep için uygun mu?
    public Guid? UnavailabilityReasonId { get; set; }// Uygun değilse nedeni (opsiyonel)
    protected Inventory()
    {
    }

   
    // Ana constructor
    public Inventory(Guid id) : base(id)
    {
    }
    // Parametreli constructor
    public Inventory(Guid id, Guid serialNumberId, Guid currentSiteId) : base(id)
    {
        SerialNumberId = serialNumberId;
        CurrentSiteId = currentSiteId;
        IsAvailableForRequest = true;
    }
    public void setid(Guid id)
    {
        this.Id = id;
    }
}