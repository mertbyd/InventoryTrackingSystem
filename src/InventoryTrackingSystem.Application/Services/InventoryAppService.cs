using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using AutoMapper;
using InventoryTrackingSystem.Dtos.Inventory;
using InventoryTrackingSystem.Interface;
using InventoryTrackingSystem.İnterface;
using InventoryTrackingSystem.Models.Inventories;
using InventoryTrackingSystem.Manager.Inventory;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Uow;

namespace InventoryTrackingSystem.Services;

public class InventoryAppService : InventoryTrackingSystemAppService, IInventoryAppService
{
    private readonly IInventoryRepository _inventoryRepository;
    private readonly InventoryManager _inventoryManager;
    private readonly ILogger<InventoryAppService> _logger;
    private readonly IMapper _mapper;
    public InventoryAppService(
        IInventoryRepository inventoryRepository,
        InventoryManager inventoryManager,
        ILogger<InventoryAppService> logger,
        IMapper mapper)
    {
        _inventoryRepository = inventoryRepository;
        _inventoryManager = inventoryManager;
        _logger = logger;
        _mapper = mapper;
    }
    // Yeni envanter oluştur
    [UnitOfWork]
    public async Task<InventoryDto> CreateAsync(CreateInventoryDto input)
    {
        // DTO -> Model
        var model = _mapper.Map<CreateInventoryModel>(input);
        // Manager ile business validation, entity oluşturma VE STOCK GÜNCELLEME
        // StockManager.IncreaseStockForNewInventoryAsync artık Manager içinde çağrılıyor
        var inventory = await _inventoryManager.CreateAsync(model);
        // Repository ile kaydet
        var createdInventory = await _inventoryRepository.InsertAsync(inventory, autoSave: true);
        // Entity -> DTO
        var result = _mapper.Map<InventoryDto>(createdInventory);
        return result;
    }
    // Envanter güncelle
    [UnitOfWork]
    public async Task<InventoryDto> UpdateAsync(Guid id, UpdateInventoryDto input)
    {
        // Mevcut envanter var mı kontrol (Manager içinde yapılıyor)
        var model = _mapper.Map<UpdateInventoryModel>(input);
        // Manager ile business validation, güncelleme VE STOCK GÜNCELLEME
        //  SerialNumber değişimi ve müsaitlik değişimi stock güncellemeleri Manager içinde
        var inventory = await _inventoryManager.UpdateAsync(id, model);
        // Repository ile kaydet
        var updatedInventory = await _inventoryRepository.UpdateAsync(inventory, autoSave: true);
        _logger.LogInformation($"Inventory updated successfully. Id: {updatedInventory.Id}");
        // Entity -> DTO
        var result = _mapper.Map<InventoryDto>(updatedInventory);
        return result;
    }
    // ID ile envanter getir
    public async Task<InventoryDto> GetAsync(Guid id)
    {
        // Repository"den getir
        var inventory = await _inventoryRepository.GetAsync(id);
        if (inventory == null)
            throw new BusinessException(InventoryTrackingSystemErrorCodes.Inventory.NotFound);
        // Entity -> DTO
        var result = _mapper.Map<InventoryDto>(inventory);
        return result;
    }
    
    // Tüm envanterleri listele
    public async Task<List<InventoryDto>> GetListAsync()
    {
        // Repository"den tüm kayıtları getir
        var inventories = await _inventoryRepository.GetListAsync();
        // Entity List -> DTO List
        var result = _mapper.Map<List<InventoryDto>>(inventories);
        return result;
    }
    
    // Envanter sil (soft delete) - GÜNCELLENDİ: Stock güncelleme eklendi
    [UnitOfWork]
    public async Task DeleteAsync(Guid id)
    {
        // Repository"den getir
        var inventory = await _inventoryRepository.GetAsync(id);
        if (inventory == null)
            throw new BusinessException(InventoryTrackingSystemErrorCodes.Inventory.NotFound);
        // Manager ile silme validasyonu VE STOCK GÜNCELLEME
        // DeleteWithStockUpdateAsync hem validasyon hem stock güncelleme yapıyor
        await _inventoryManager.DeleteWithStockUpdateAsync(id);
        // Inventory"yi sil (soft delete)
        await _inventoryRepository.DeleteAsync(inventory, autoSave: true);
        _logger.LogInformation($"Inventory deleted successfully. Id: {id}, SerialNumber: {inventory.SerialNumberId}");
    }
    
    // Müsaitlik durumunu değiştir
    [UnitOfWork]
    public async Task<InventoryDto> ChangeAvailabilityStatusAsync(ChangeAvailabilityStatusDto input)
    {
        // DTO -> Model
        var model = _mapper.Map<ChangeAvailabilityStatusModel>(input);
        // Manager ile durum değişikliği VE STOCK GÜNCELLEME
        var inventory = await _inventoryManager.ChangeAvailabilityAsync(
            input.InventoryId, 
            model
        );
        // Repository ile kaydet
        var updatedInventory = await _inventoryRepository.UpdateAsync(inventory, autoSave: true);
        _logger.LogInformation($"Inventory availability changed. Id: {input.InventoryId}, NewStatus: {model.IsAvailable}");
        // Entity -> DTO
        var result = _mapper.Map<InventoryDto>(updatedInventory);
        return result;
    }
}