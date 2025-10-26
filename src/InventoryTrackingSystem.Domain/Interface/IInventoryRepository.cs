using System;
using System.Collections.Generic;
using InventoryTrackingSystem.Entities;
using Volo.Abp.Domain.Repositories;
using System.Threading.Tasks;
using InventoryTrackingSystem.Models.Stock;

namespace InventoryTrackingSystem.Interface;

public interface IInventoryRepository:IRepository<Inventory,Guid>
{
    public Task<Inventory> ChangeAvailabilityStatusAsync(Guid inventoryId, bool isAvailable,
        Guid? unavailabilityReasonId = null);// Kullanım durumunu değiştir
    // Stock özeti için GROUP BY sorgusu - Dictionary döner
    
    public  Task<List<StockSummary>> GetStockSummaryAsync();
}

