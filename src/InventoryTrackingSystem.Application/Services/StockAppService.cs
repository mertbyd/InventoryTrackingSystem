// Services/StockAppService.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.Logging;
using AutoMapper;
using InventoryTrackingSystem.Entities;
using InventoryTrackingSystem.Interface;
using InventoryTrackingSystem.Dtos.Stock;
using InventoryTrackingSystem.İnterface;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Uow;

namespace InventoryTrackingSystem.Services;

public class StockAppService : InventoryTrackingSystemAppService, IStockAppService
{
    private readonly IRepository<Stock, Guid> _stockRepository;
    private readonly IInventoryRepository _inventoryRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<StockAppService> _logger;

    public StockAppService(
        IRepository<Stock, Guid> stockRepository,
        IInventoryRepository inventoryRepository,
        IMapper mapper,
        ILogger<StockAppService> logger)
    {
        _stockRepository = stockRepository;
        _inventoryRepository = inventoryRepository;
        _mapper = mapper;
        _logger = logger;
    }
[UnitOfWork]
public async Task<List<StockDto>> GetStockListAsync()
{
    //  Inventory'den özet al
    var stockSummaries = await _inventoryRepository.GetStockSummaryAsync();
    //  Mevcut stock'ları al
    var existingStocks = await _stockRepository.GetListAsync();
    var processedSerialNumbers = new HashSet<Guid>();
    var resultStocks = new List<Stock>(); // İşlenen ve güncellenen tüm Stock nesnelerini tutacak liste
    // Stock'ları güncelle/oluştur
    foreach (var summary in stockSummaries)
    {
        var existingStock = existingStocks.FirstOrDefault(s => s.SerialNumberId == summary.SerialNumberId);
        if (existingStock != null)
        {
            // Güncelle
            if (existingStock.TotalProduct != summary.TotalCount ||
                existingStock.AvailableProduct != summary.AvailableCount)
            {
                existingStock.UpdateStock(summary.TotalCount, summary.AvailableCount);
                await _stockRepository.UpdateAsync(existingStock, autoSave: false);
            }
            resultStocks.Add(existingStock);
            processedSerialNumbers.Add(summary.SerialNumberId); // İşlenen SN'yi işaretle
        }
        else
        {
            // Yeni oluştur
            var newStock = new Stock(Guid.NewGuid(), summary.SerialNumberId);
            newStock.UpdateStock(summary.TotalCount, summary.AvailableCount);
            // InsertAsync, yeni oluşturulan Stock nesnesini resultStocks'a eklemeden önce kaydeder
            resultStocks.Add(await _stockRepository.InsertAsync(newStock, autoSave: false));
            processedSerialNumbers.Add(summary.SerialNumberId); // İşlenen SN'yi işaretle
        }
    }
    // 4. Orphan stock'ları sıfırla (Döngü bittikten sonra yapılmalı)
    // Inventory'de kaydı kalmayan, ancak Stock tablosunda 0'dan büyük bakiyesi olanlar.
    foreach (var orphan in existingStocks.Where(s => !processedSerialNumbers.Contains(s.SerialNumberId)))
    {
        if (orphan.TotalProduct != 0 || orphan.AvailableProduct != 0)
        {
            orphan.UpdateStock(0, 0);
            await _stockRepository.UpdateAsync(orphan, autoSave: false);
        }
        resultStocks.Add(orphan);
    }
    // Değişiklikleri veritabanına kaydet (Döngü ve tüm işlemler bittikten sonra)
    await CurrentUnitOfWork.SaveChangesAsync();
    // 6. Sonuçları DTO'ya dönüştür ve filtrele (EN SONDA)
    return _mapper.Map<List<StockDto>>(resultStocks)
        .Where(x => x.TotalProduct > 0) // Yalnızca stoğu olanları göster
        .OrderBy(x => x.SerialNumberId)
        .ToList();
}
}