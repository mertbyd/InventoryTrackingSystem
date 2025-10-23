using System;
using InventoryTrackingSystem.Entities;
using Volo.Abp.Domain.Repositories;
using System.Threading.Tasks;

namespace InventoryTrackingSystem.Interface;

public interface IInventoryRepository:IRepository<Inventory>
{
    public Task<Inventory> ChangeAvailabilityStatusAsync(Guid inventoryId, bool isAvailable,
        Guid? unavailabilityReasonId = null);// Kullanım durumunu değiştir

}