using System;

namespace InventoryTrackingSystem.Dtos.Inventory;

public class ChangeAvailabilityStatusDto
{
    public Guid InventoryId { get; set; }
    
    // Yeni durum
    public bool IsAvailable { get; set; }
    
    // Uygun değilse sebep
    public Guid? UnavailabilityReasonId { get; set; }
}