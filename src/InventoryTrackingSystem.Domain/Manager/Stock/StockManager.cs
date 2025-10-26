using System;
using System.Threading.Tasks;
using InventoryTrackingSystem.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Microsoft.Extensions.Logging;
using Volo.Abp;

namespace InventoryTrackingSystem.Manager.Stock;

public class StockManager : DomainService
{
    private readonly IRepository<Entities.Stock, Guid> _stockRepository;
    private readonly IRepository<SerialNumber, Guid> _serialNumberRepository;
    private readonly ILogger<StockManager> _logger;

    public StockManager(
        IRepository<Entities.Stock, Guid> stockRepository,
        IRepository<SerialNumber, Guid> serialNumberRepository,
        ILogger<StockManager> logger)
    {
        _stockRepository = stockRepository;
        _serialNumberRepository = serialNumberRepository;
        _logger = logger;
    }
    // SerialNumber'ın var olup olmadığını kontrol eden fonksiyon
    private async Task<SerialNumber> ValidateSerialNumberAsync(Guid serialNumberId)
    {
        var serialNumber = await _serialNumberRepository.FindAsync(serialNumberId);
        if (serialNumber == null)
            throw new BusinessException(InventoryTrackingSystemErrorCodes.SerialNumber.NotFound);
        return serialNumber;
    }
    // Stock kaydını getiren fonksiyon (yoksa null döner)
    private async Task<Entities.Stock?> GetStockBySerialNumberAsync(Guid serialNumberId)
    {
        return await _stockRepository.FirstOrDefaultAsync(x => x.SerialNumberId == serialNumberId);
    }
    // Yeni Stock kaydı oluşturan fonksiyon
    private async Task<Entities.Stock> CreateNewStockAsync(Guid serialNumberId, int totalProduct = 0, int availableProduct = 0)
    {
        // Önce SerialNumber'ın varlığını kontrol et
        await ValidateSerialNumberAsync(serialNumberId);
        // Yeni stock oluştur
        var newStock = new Entities.Stock(Guid.NewGuid(), serialNumberId);
        newStock.TotalProduct = totalProduct;
        newStock.AvailableProduct = availableProduct;
        // Veritabanına kaydet
        var createdStock = await _stockRepository.InsertAsync(newStock, autoSave: true);
        return createdStock;
    }
    // Stock kaydını getiren veya yoksa oluşturan fonksiyon
    private async Task<Entities.Stock> GetOrCreateStockAsync(Guid serialNumberId)
    {
        // Önce mevcut stock'u ara
        var stock = await GetStockBySerialNumberAsync(serialNumberId);
        // Stock yoksa yeni oluştur
        if (stock == null)
            stock = await CreateNewStockAsync(serialNumberId, 0, 0);
        return stock;
    }
    // Stock miktarlarını güncelleyen yardımcı fonksiyon
    private async Task UpdateStockQuantitiesAsync(Entities.Stock stock, int totalChange, int availableChange)
    {
        // Total product güncelleme
        stock.TotalProduct += totalChange;
        // Negatif stok kontrolü
        if (stock.TotalProduct < 0)
            throw new BusinessException(InventoryTrackingSystemErrorCodes.Stock.NegativeStock);
        // Available product güncelleme
        stock.AvailableProduct += availableChange;
        // Available, Total'dan fazla olamaz
        if (stock.AvailableProduct > stock.TotalProduct)
            stock.AvailableProduct = stock.TotalProduct;
        // Available negatif olamaz
        if (stock.AvailableProduct < 0)
            stock.AvailableProduct = 0;
        await _stockRepository.UpdateAsync(stock, autoSave: true);
    }
    // Inventory oluşturulduğunda çağrılacak
    // NOT: Inventory müsait değil olarak da yaratılabilir!
    public async Task IncreaseStockForNewInventoryAsync(Guid serialNumberId, bool isAvailable)
    {
        // Stock'u getir veya oluştur
        var stock = await GetOrCreateStockAsync(serialNumberId);
        // Her durumda toplam stok 1 artar
        // Ama sadece müsait ise available stok artar
        int totalIncrease = 1;
        int availableIncrease = isAvailable ? 1 : 0;
        await UpdateStockQuantitiesAsync(stock, totalIncrease, availableIncrease);
    }
    // Inventory silindiğinde çağrılacak
    public async Task DecreaseStockForDeletedInventoryAsync(Guid serialNumberId, bool wasAvailable)
    {
        // Stock kaydını bul
        var stock = await GetStockBySerialNumberAsync(serialNumberId);
        if (stock == null)
            throw new BusinessException(InventoryTrackingSystemErrorCodes.Stock.NotFound);
        // Her durumda toplam stok 1 azalır
        // Ama sadece müsait idiyse available stok azalır
        int totalDecrease = -1;
        int availableDecrease = wasAvailable ? -1 : 0;
        await UpdateStockQuantitiesAsync(stock, totalDecrease, availableDecrease);
    }

    // Inventory müsaitlik durumu değiştiğinde çağrılacak - DAHA ANLAŞILIR VERSİYON
    public async Task UpdateStockAvailabilityAsync(Guid serialNumberId, bool previousAvailability, bool newAvailability)
    {
        // Durum değişmemişse işlem yapma
        if (previousAvailability == newAvailability)
            return;
        var stock = await GetStockBySerialNumberAsync(serialNumberId);
        if (stock == null)
            throw new BusinessException(InventoryTrackingSystemErrorCodes.Stock.NotFound);
        // Müsaitlik durumu değişimini hesapla
        if (newAvailability == true)
            // Envanter müsait hale geldi -> Available stok ARTAR
            await UpdateStockQuantitiesAsync(stock, 0, 1);
        
        else
            // Envanter müsait olmaktan çıktı -> Available stok AZALIR
            await UpdateStockQuantitiesAsync(stock, 0, -1);
        
    }

    // Alternatif: Daha da basit versiyon
    public async Task SimpleUpdateAvailabilityAsync(Guid serialNumberId, bool becomingAvailable)
    {
        var stock = await GetStockBySerialNumberAsync(serialNumberId);
        if (stock == null)
            throw new BusinessException(InventoryTrackingSystemErrorCodes.Stock.NotFound);
        if (becomingAvailable)
        {
            stock.AvailableProduct += 1;
            if (stock.AvailableProduct > stock.TotalProduct)
                stock.AvailableProduct = stock.TotalProduct;
        }
        else
        {
            if (stock.AvailableProduct > 0)
                stock.AvailableProduct -= 1;
        }
        await _stockRepository.UpdateAsync(stock, autoSave: true);
    }

    // Stock kontrolü için yardımcı method
    public async Task<bool> HasSufficientStockAsync(Guid serialNumberId, int requiredQuantity)
    {
        var stock = await GetStockBySerialNumberAsync(serialNumberId);
        if (stock == null)
            return false;
        return stock.AvailableProduct >= requiredQuantity;
    }
    // Stock bilgisini getir 
    public async Task<Entities.Stock?> GetStockInfoAsync(Guid serialNumberId)
    {
        return await GetStockBySerialNumberAsync(serialNumberId);
    }
}