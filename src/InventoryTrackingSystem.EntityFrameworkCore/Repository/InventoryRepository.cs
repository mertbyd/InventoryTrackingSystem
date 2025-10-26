using System;
using System.Collections.Generic;
using System.Linq;
using InventoryTrackingSystem.Entities;
using InventoryTrackingSystem.EntityFrameworkCore;
using InventoryTrackingSystem.Interface;
using Volo.Abp.EntityFrameworkCore;
using System.Threading.Tasks;
using InventoryTrackingSystem.Models.Stock;
using Microsoft.EntityFrameworkCore;

namespace InventoryTrackingSystem.Repository;

public class InventoryRepository:BaseRepository<Inventory>,IInventoryRepository
{
    public InventoryRepository(IDbContextProvider<InventoryTrackingSystemDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }

    // hangeAvailabilityStatusAsync
    public async Task<Inventory> ChangeAvailabilityStatusAsync(
        Guid inventoryId, 
        bool isAvailable, 
        Guid? unavailabilityReasonId = null)
    {
        var dbContext = await GetDbContextAsync();
        var inventory = await dbContext.Inventories
            .FirstOrDefaultAsync(x => x.Id == inventoryId);
        if (inventory == null)
        {
            throw new Exception($"Inventory with id {inventoryId} not found");
        }
        // Durumu güncelle
        inventory.IsAvailableForRequest = isAvailable;
        // Eğer uygun değilse ve sebep verilmişse
        if (!isAvailable && unavailabilityReasonId.HasValue)
        {
            inventory.UnavailabilityReasonId = unavailabilityReasonId;
        }
        else if (isAvailable)
        {
            // Uygunsa sebebi temizle
            inventory.UnavailabilityReasonId = null;
        }
        await dbContext.SaveChangesAsync();
        return inventory;
    } 
    public async Task<List<StockSummary>> GetStockSummaryAsync()
    {
        var dbContext = await GetDbContextAsync();
        var stockSummaries = await dbContext.Set<Inventory>()
            .Where(x => !x.IsDeleted)
            .GroupBy(x => x.SerialNumberId)
            .Select(g => new StockSummary // Domain model kullanıyoruz
            {
                SerialNumberId = g.Key,
                TotalCount = g.Count(),
                AvailableCount = g.Count(x => x.IsAvailableForRequest)
            })
            .ToListAsync();
        return stockSummaries;
    }
}