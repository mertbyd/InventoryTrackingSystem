using System;

namespace InventoryTrackingSystem.Dtos.Inventory;

public class InventoryDto
{
    public Guid SerialNumberId { get; set; }// Seri numarası ön eki (Grup adı: A, B, C)
    public string Name { get; set; } = string.Empty;// Envanter adı (Kürek, Levha, Kazma)
    public Guid CurrentSiteId { get; set; }// Envanterin şu an bulunduğu lokasyon
    public bool IsAvailableForRequest { get; set; } = true;// Yeni talep için uygun mu?
    public Guid? UnavailabilityReasonId { get; set; }// Uygun değilse nedeni (opsiyonel)
}