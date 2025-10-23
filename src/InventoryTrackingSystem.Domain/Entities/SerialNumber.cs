using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace InventoryTrackingSystem.Entities;

public class SerialNumber:FullAuditedEntity<Guid>
{
    public string Name { get; set; } = string.Empty;// Seri numarası grubu adı (Kürek, Levha, Kazma)
    public string SerialNumberPrefix { get; set; } = string.Empty;// Seri numarası 
    protected SerialNumber()
    {
    }
    
    // Ana constructor
    public SerialNumber(Guid id) : base(id)
    {
    }
    
    // Parametreli constructor
    public SerialNumber(Guid id, string name, string serialNumberPrefix) : base(id)
    {
        Name = name;
        SerialNumberPrefix = serialNumberPrefix;
    }
    
}