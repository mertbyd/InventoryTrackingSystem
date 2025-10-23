using System;

namespace InventoryTrackingSystem.Models.Inventories;

public class UpdateInventoryModel
{
    public Guid Id { get; set; }
    public Guid SerialNumberId { get; set; }
    public Guid CurrentSiteId { get; set; }
    public bool IsAvailableForRequest { get; set; }
    public Guid? UnavailabilityReasonId { get; set; }
}