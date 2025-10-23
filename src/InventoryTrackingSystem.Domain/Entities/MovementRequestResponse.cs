using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace InventoryTrackingSystem.Entities;

public class MovementRequestResponse:FullAuditedEntity<Guid>
{
    public Guid MovementRequestId { get; set; }// İlişkili talep ID
    public Guid ApproverUserId { get; set; }// Onaylayan/Reddeden kullanıcı
    public bool IsApproved { get; set; }// Onay durumu (true: Onaylandı, false: Reddedildi)
    public string? Comment { get; set; }// Onay/Red açıklaması (opsiyonel)
    protected MovementRequestResponse()
    {
    }
    
    // Ana constructor
    public MovementRequestResponse(Guid id) : base(id)
    {
    }
    
    // Parametreli constructor
    public MovementRequestResponse(Guid id, Guid movementRequestId, Guid approverUserId, bool isApproved, string? comment = null) : base(id)
    {
        MovementRequestId = movementRequestId;
        ApproverUserId = approverUserId;
        IsApproved = isApproved;
        Comment = comment;
    }
}