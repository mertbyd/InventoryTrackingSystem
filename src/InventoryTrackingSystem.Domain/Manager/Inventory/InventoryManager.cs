using System;
using System.Threading.Tasks;
using System.Linq;
using InventoryTrackingSystem.Entities;
using InventoryTrackingSystem.Interface;
using InventoryTrackingSystem.Models.Inventories;
using InventoryTrackingSystem.Manager.Stock; // StockManager için
using AutoMapper;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;

namespace InventoryTrackingSystem.Manager.Inventory;

public class InventoryManager : BaseManager<Entities.Inventory>
{
    private readonly IInventoryMovementRepository _inventoryMovementRepository;
    private readonly IRepository<SerialNumber, Guid> _serialNumberRepository;
    private readonly IRepository<Site, Guid> _siteRepository;
    private readonly IRepository<UnavailabilityReason, Guid> _unavailabilityReasonRepository;
    private readonly StockManager _stockManager; // EKLENEN: StockManager
    private readonly IMapper _mapper;
    
    public InventoryManager(
        IInventoryRepository inventoryRepository,
        IInventoryMovementRepository inventoryMovementRepository,
        IRepository<SerialNumber, Guid> serialNumberRepository,
        IRepository<Site, Guid> siteRepository,
        IRepository<UnavailabilityReason, Guid> unavailabilityReasonRepository,
        StockManager stockManager, // EKLENEN: Constructor'a StockManager
        IMapper mapper)
        : base(inventoryRepository)
    {
        _inventoryMovementRepository = inventoryMovementRepository;
        _serialNumberRepository = serialNumberRepository;
        _siteRepository = siteRepository;
        _unavailabilityReasonRepository = unavailabilityReasonRepository;
        _stockManager = stockManager; // EKLENEN: StockManager ataması
        _mapper = mapper;
    }
    #region CREATE - Yeni envanter oluştur
    public async Task<Entities.Inventory> CreateAsync(CreateInventoryModel model)
    {
        // CREATE VALIDATIONs
        await ValidateForCreateAsync(model);
        // Entity oluştur
        var inventory = _mapper.Map<Entities.Inventory>(model);
        inventory.setid(GuidGenerator.Create());
        // EKLENEN: Stock güncelleme - Yeni inventory için stock artır
        await _stockManager.IncreaseStockForNewInventoryAsync(
            model.SerialNumberId,
            model.IsAvailableForRequest
        );
        return inventory;
    }
    private async Task ValidateForCreateAsync(CreateInventoryModel model)
    {
        // SerialNumber null değilse kontrol et
        if (model.SerialNumberId != Guid.Empty)
        {
            var serialNumber = await _serialNumberRepository.FindAsync(model.SerialNumberId);
            if (serialNumber == null)
                throw new BusinessException(InventoryTrackingSystemErrorCodes.SerialNumber.NotFound);
        }
        //  CurrentSite zorunlu - var mı kontrol et
        var site = await _siteRepository.FindAsync(model.CurrentSiteId);
        if (site == null)
            throw new BusinessException(InventoryTrackingSystemErrorCodes.Site.NotFound);
        //  Müsaitlik logic kontrolü
        await ValidateAvailabilityLogicAsync(model.IsAvailableForRequest, model.UnavailabilityReasonId);
    }
    #endregion
    #region UPDATE - Envanter güncelle
    public async Task<Entities.Inventory> UpdateAsync(Guid id, UpdateInventoryModel model)
    {
        // Envanteri getir
        var inventory = await Repository.GetAsync(id);
        if (inventory == null)
            throw new BusinessException(InventoryTrackingSystemErrorCodes.Inventory.NotFound);
        // Güncelleme öncesi eski değerleri sakla (stock için)
        var oldSerialNumberId = inventory.SerialNumberId;
        var oldAvailability = inventory.IsAvailableForRequest;
        // UPDATE VALIDATIONs
        await ValidateForUpdateAsync(id, model);
        //  Stock güncelleme mantığı
        // SerialNumber değişti mi?
        if (oldSerialNumberId != model.SerialNumberId)
        {
            // Eski SerialNumber için stock azalt
            await _stockManager.DecreaseStockForDeletedInventoryAsync(oldSerialNumberId, oldAvailability);
            // Yeni SerialNumber için stock artır
            await _stockManager.IncreaseStockForNewInventoryAsync(model.SerialNumberId, model.IsAvailableForRequest);
        }
        // Sadece müsaitlik durumu değişti mi?
        else if (oldAvailability != model.IsAvailableForRequest)
        {
            // Stock müsaitlik güncellemesi
            await _stockManager.UpdateStockAvailabilityAsync(
                model.SerialNumberId,
                oldAvailability,
                model.IsAvailableForRequest
            );
        }
        // Güncelle ve döndür
        _mapper.Map(model, inventory);
        return inventory;
    }

    private async Task ValidateForUpdateAsync(Guid inventoryId, UpdateInventoryModel model)
    {
        // Hareket halinde mi kontrol et
        await CheckNotInMovementAsync(inventoryId);
        //  SerialNumber değiştiyse kontrol et
        if (model.SerialNumberId != Guid.Empty)
        {
            var serialNumber = await _serialNumberRepository.FindAsync(model.SerialNumberId);
            if (serialNumber == null)
                throw new BusinessException(InventoryTrackingSystemErrorCodes.SerialNumber.NotFound);
        }
        //  Site değiştiyse kontrol et
        var site = await _siteRepository.FindAsync(model.CurrentSiteId);
        if (site == null)
            throw new BusinessException(InventoryTrackingSystemErrorCodes.Site.NotFound);
        //  Müsaitlik logic kontrolü
        await ValidateAvailabilityLogicAsync(model.IsAvailableForRequest, model.UnavailabilityReasonId);
    }
    #endregion
    #region DELETE - Envanter sil
    public async Task ValidateForDeleteAsync(Guid id)
    {
        // 1. Envanter var mı
        var inventory = await Repository.FindAsync(id);
        if (inventory == null)
            throw new BusinessException(InventoryTrackingSystemErrorCodes.Inventory.NotFound);
        // 2. Hareket halinde mi
        await CheckNotInMovementAsync(id);
    }
    
    // EKLENEN: Delete işlemi için yeni metod
    public async Task DeleteWithStockUpdateAsync(Guid id)
    {
        // Inventory'yi getir
        var inventory = await Repository.GetAsync(id);
        if (inventory == null)
            throw new BusinessException(InventoryTrackingSystemErrorCodes.Inventory.NotFound);
        
        // Hareket halinde mi kontrol et
        await CheckNotInMovementAsync(id);
        // Stock güncelleme - Silinen inventory için stock azalt
        await _stockManager.DecreaseStockForDeletedInventoryAsync(
            inventory.SerialNumberId,
            inventory.IsAvailableForRequest
        );
    }
    #endregion
    #region CHANGE AVAILABILITY - Sadece durum değiştir
    public async Task<Entities.Inventory> ChangeAvailabilityAsync(Guid id, ChangeAvailabilityStatusModel model)
    {
        // Envanteri getir
        var inventory = await Repository.GetAsync(id);
        if (inventory == null)
            throw new BusinessException(InventoryTrackingSystemErrorCodes.Inventory.NotFound);
        // Eski müsaitlik durumunu sakla
        var oldAvailability = inventory.IsAvailableForRequest;
        // CHANGE VALIDATIONs
        await ValidateForChangeAvailabilityAsync(id, model);
        //  Stock müsaitlik güncellemesi (sadece durum değiştiyse)
        if (oldAvailability != model.IsAvailable)
        {
            await _stockManager.UpdateStockAvailabilityAsync(
                inventory.SerialNumberId,
                oldAvailability,
                model.IsAvailable
            );
        }
        // Sadece durumu güncelle
        inventory.IsAvailableForRequest = model.IsAvailable;
        inventory.UnavailabilityReasonId = model.UnavailabilityReasonId;
        
        return inventory;
    }
    private async Task ValidateForChangeAvailabilityAsync(Guid inventoryId, ChangeAvailabilityStatusModel model)
    {
        // Hareket halinde mi
        await CheckNotInMovementAsync(inventoryId);
        //  Müsaitlik logic kontrolü
        await ValidateAvailabilityLogicAsync(model.IsAvailable, model.UnavailabilityReasonId);
    }
    #endregion
    #region ORTAK KONTROLLER
    // Müsaitlik mantığı - true ise sebep null, false ise sebep zorunlu
    private async Task ValidateAvailabilityLogicAsync(bool isAvailable, Guid? reasonId)
    {
        if (isAvailable && reasonId.HasValue)
        {
            // Müsaitse sebep OLAMAZ
            throw new BusinessException(
                InventoryTrackingSystemErrorCodes.General.InvalidOperation);
        }
        if (!isAvailable && !reasonId.HasValue)
        {
            // Müsait değilse sebep ZORUNLU
            throw new BusinessException(
                InventoryTrackingSystemErrorCodes.Inventory.ReasonRequiredWhenUnavailable);
        }
        // Sebep ID varsa veritabanında var mı kontrol et
        if (reasonId.HasValue)
        {
            var reason = await _unavailabilityReasonRepository.FindAsync(reasonId.Value);
            if (reason == null)
                throw new BusinessException(InventoryTrackingSystemErrorCodes.UnavailabilityReason.NotFound);
        }
    }
    // Hareket halinde mi kontrolü
    private async Task CheckNotInMovementAsync(Guid inventoryId)
    {
        var movements = await _inventoryMovementRepository.GetListAsync();
        var hasActiveMovement = movements.Any(x => 
            x.InventoryId == inventoryId && 
            !x.IsCompleted);
        
        if (hasActiveMovement)
        {
            throw new BusinessException(
                InventoryTrackingSystemErrorCodes.Inventory.CannotUpdateInMovement);
        }
    }
    #endregion
}