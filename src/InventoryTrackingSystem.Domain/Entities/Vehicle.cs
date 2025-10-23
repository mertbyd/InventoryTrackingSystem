using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace InventoryTrackingSystem.Entities;

public class Vehicle:FullAuditedEntity<Guid>
{
    public string PlateNumber { get; set; } = string.Empty;// Araç plaka numarası (34ABC123)
    public string Type { get; set; } = string.Empty;// Araç tipi (KAMYONET, BINEK, KAMYON, TIR)
    protected Vehicle()
    {
    } public Vehicle(Guid id) : base(id)
    {
    }
    public Vehicle(Guid id, string plateNumber, string type) : base(id)
    {
        PlateNumber = plateNumber;
        Type = type;
    }
}