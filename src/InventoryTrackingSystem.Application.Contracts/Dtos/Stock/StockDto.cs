// Dtos/Stock/StockDto.cs
using System;

namespace InventoryTrackingSystem.Dtos.Stock;

public class StockDto
{
    public Guid? Id { get; set; } // Nullable - GROUP BY sonucunda null olabilir
    public Guid SerialNumberId { get; set; }
    public int TotalProduct { get; set; }
    public int AvailableProduct { get; set; }

}