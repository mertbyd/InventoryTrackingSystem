using System;
using Volo.Abp.Domain.Entities;

namespace InventoryTrackingSystem.Entities;

public class MovementRequestDetail : Entity<Guid>
{
    public Guid MovementRequestId { get; set; }    // İlişkili talep ID
    public Guid RequestedSerialNumberId { get; set; }    // Talep edilen seri numarası ID (SerialNumber tablosundan)
    public int RequestedQuantity { get; set; }    // Talep edilen miktar
    
    // EF Core için protected constructor
    protected MovementRequestDetail()
    {
    }
    
    // Ana constructor
    public MovementRequestDetail(Guid id) : base(id)
    {
    }
    
    // Parametreli constructor
    public MovementRequestDetail(Guid id, Guid movementRequestId, Guid requestedSerialNumberId, int requestedQuantity) : base(id)
    {
        MovementRequestId = movementRequestId;
        RequestedSerialNumberId = requestedSerialNumberId;
        RequestedQuantity = requestedQuantity;
    }
}