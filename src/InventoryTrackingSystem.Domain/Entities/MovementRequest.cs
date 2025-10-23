using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace InventoryTrackingSystem.Entities;

public class MovementRequest:FullAuditedEntity<Guid>
{
    public Guid RequesterUserId { get; set; }// Talebi oluşturan kullanıcı
    public Guid FromSiteId { get; set; }// Kaynak lokasyon (envanterin alınacağı yer)
    public Guid ToSiteId { get; set; }// Hedef lokasyon (envanterin gideceği yer)
    public string Status { get; set; } = "PENDING";// Talep durumu (PENDING, APPROVED, REJECTED, COMPLETED)
    protected MovementRequest()
    {
    }
    
    // Ana constructor
    public MovementRequest(Guid id) : base(id)
    {
    }
    
    // Parametreli constructor
    public MovementRequest(Guid id, Guid requesterUserId, Guid fromSiteId, Guid toSiteId) : base(id)
    {
        RequesterUserId = requesterUserId;
        FromSiteId = fromSiteId;
        ToSiteId = toSiteId;
        Status = "PENDING";
    }
    
}