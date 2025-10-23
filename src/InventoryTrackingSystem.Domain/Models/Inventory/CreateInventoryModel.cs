using System;
namespace InventoryTrackingSystem.Models.Inventories;
public class CreateInventoryModel
{
    public Guid SerialNumberId { get; set; }
    public Guid CurrentSiteId { get; set; }
    public bool IsAvailableForRequest { get; set; }
    public Guid? UnavailabilityReasonId { get; set; }
}