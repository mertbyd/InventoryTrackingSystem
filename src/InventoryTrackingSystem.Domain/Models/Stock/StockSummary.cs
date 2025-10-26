using System;

namespace InventoryTrackingSystem.Models.Stock;

public class StockSummary
{
    public Guid SerialNumberId { get; set; }
    public int TotalCount { get; set; }
    public int AvailableCount { get; set; }
}