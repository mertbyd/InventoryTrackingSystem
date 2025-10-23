using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace InventoryTrackingSystem.Entities;

public class InventoryMovement:FullAuditedEntity<Guid>
{
    public Guid MovementRequestId { get; set; }// İlişkili talep ID
    public Guid InventoryId { get; set; }// Hareketi yapılan tekil envanter ID
    public Guid MovementRequestDetailId { get; set; }// İlişkili talep detay ID
    public string MovementStatus { get; set; } = "DEPARTED";// Hareket durumu (DEPARTED, DELIVERED, RETURNED)
    public bool IsCompleted { get; set; } = false;// Hareket tamamlandı mı?
    public Guid? VehicleId { get; set; }// Transfer aracı ID (site transferi ise zorunlu)
    protected InventoryMovement()
    {
    } public InventoryMovement(Guid id) : base(id)
    {
    } // Parametreli constructor
    public InventoryMovement(Guid id, Guid movementRequestId, Guid inventoryId, Guid movementRequestDetailId, Guid? vehicleId = null) : base(id)
    {
        MovementRequestId = movementRequestId;
        InventoryId = inventoryId;
        MovementRequestDetailId = movementRequestDetailId;
        MovementStatus = "DEPARTED";
        IsCompleted = false;
        VehicleId = vehicleId;
    }
}