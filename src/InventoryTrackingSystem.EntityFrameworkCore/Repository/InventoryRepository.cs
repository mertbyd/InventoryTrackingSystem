using System;
using System.Linq;
using InventoryTrackingSystem.Entities;
using InventoryTrackingSystem.EntityFrameworkCore;
using InventoryTrackingSystem.Interface;
using Volo.Abp.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace InventoryTrackingSystem.Repository;

public class InventoryRepository:BaseRepository<Inventory>,IInventoryRepository
{
    public InventoryRepository(IDbContextProvider<InventoryTrackingSystemDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }

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
}