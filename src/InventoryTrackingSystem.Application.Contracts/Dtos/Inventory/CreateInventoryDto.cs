using System;

namespace InventoryTrackingSystem.Dtos.Inventory;

public class CreateInventoryDto
{
    public Guid SerialNumberId { get; set; }
    
    // Başlangıç lokasyonu
    public Guid CurrentSiteId { get; set; }
    
    // Başlangıçta müsait mi (default: true)
    public bool IsAvailableForRequest { get; set; } = true;
    
    // Müsait değilse sebebi (opsiyonel)
    public Guid? UnavailabilityReasonId { get; set; }
}