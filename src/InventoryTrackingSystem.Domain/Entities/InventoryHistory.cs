using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace InventoryTrackingSystem.Entities;

public class InventoryHistory:FullAuditedEntity<Guid>
{
    public Guid InventoryId { get; set; }// İlişkili envanter ID
    public Guid? MovementId { get; set; }// İlişkili hareket ID (opsiyonel)
    public string EventType { get; set; } = string.Empty;// Olay tipi
    public string Description { get; set; } = string.Empty;// Olay açıklaması
    public Guid? UserId { get; set; }// İşlemi yapan kullanıcı (opsiyonel)
    // EF Core için protected constructor
    protected InventoryHistory()
    {
    }
    // Ana constructor
    public InventoryHistory(Guid id) : base(id)
    {
    }
    // Parametreli constructor
    public InventoryHistory(Guid id, Guid inventoryId, string eventType, string description, Guid? movementId = null, Guid? userId = null) : base(id)
    {
        InventoryId = inventoryId;
        EventType = eventType;
        Description = description;
        MovementId = movementId;
        UserId = userId;
    }
}