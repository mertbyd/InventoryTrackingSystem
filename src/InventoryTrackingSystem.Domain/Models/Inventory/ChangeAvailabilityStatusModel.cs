using System;

namespace InventoryTrackingSystem.Models.Inventories;

public class ChangeAvailabilityStatusModel
{
    public Guid InventoryId { get; set; }
    public bool IsAvailable { get; set; }
    public Guid? UnavailabilityReasonId { get; set; }
}