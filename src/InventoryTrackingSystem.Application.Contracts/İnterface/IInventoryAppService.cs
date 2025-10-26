using System;
using System.Collections.Generic;
using InventoryTrackingSystem.Dtos.Inventory;
using Volo.Abp.Application.Services;
using System.Threading.Tasks;
namespace InventoryTrackingSystem.İnterface;

public  interface IInventoryAppService:IApplicationService
{
    // Envanter oluştur
   Task<InventoryDto> CreateAsync(CreateInventoryDto input);
    // Envanter güncelle
    Task<InventoryDto> UpdateAsync(Guid id, UpdateInventoryDto input);
    // ID ile envanter getir
    Task<InventoryDto> GetAsync(Guid id);
    // Tüm envanterleri listele
    Task<List<InventoryDto>> GetListAsync();
    // Envanter sil
    Task DeleteAsync(Guid id);
    // Müsaitlik durumunu değiştir
    Task<InventoryDto> ChangeAvailabilityStatusAsync(ChangeAvailabilityStatusDto input);
}