using System;

namespace InventoryTrackingSystem.Dtos.Inventory;

public class UpdateInventoryDto
{
    // Seri numarası değişebilir (ürün tipi değişimi)
    public Guid SerialNumberId { get; set; }
    
    // Lokasyon değişikliği
    public Guid CurrentSiteId { get; set; }
    
    // Müsaitlik durumu
    public bool IsAvailableForRequest { get; set; }
    
    // Uygunsuzluk sebebi
    public Guid? UnavailabilityReasonId { get; set; }
}