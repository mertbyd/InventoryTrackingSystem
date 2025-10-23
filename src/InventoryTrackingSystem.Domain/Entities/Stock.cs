using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace InventoryTrackingSystem.Entities;

public class Stock : FullAuditedEntity<Guid>
{

   
    public Guid SerialNumberId { get; set; } // İlişkili seri numarası ID
    public int TotalProduct { get; set; } // Toplam ürün sayısı (IsDeleted=false olan Inventory kayıtları)
    public int AvailableProduct { get; set; }    // Müsait ürün sayısı (IsAvailableForRequest=true ve IsDeleted=false olanlar)
    // EF Core için protected constructor
    protected Stock()
    {
    }
    // Ana constructor
    public Stock(Guid id) : base(id)
    {
    }
    // Parametreli constructor
    public Stock(Guid id, Guid serialNumberId) : base(id)
    {
        SerialNumberId = serialNumberId;
        TotalProduct = 0;
        AvailableProduct = 0;
    }
    // Stok güncelleme metodu
    public void UpdateStock(int totalProduct, int availableProduct)
    {
        TotalProduct = totalProduct;
        AvailableProduct = availableProduct;
    }
    // Müsait ürün azaltma metodu
    public void DecreaseAvailableProduct(int quantity)
    {
        if (AvailableProduct >= quantity)
        {
            AvailableProduct -= quantity;
        }
        else
        {
            throw new InvalidOperationException($"Yetersiz stok! Müsait: {AvailableProduct}, İstenen: {quantity}");
        }
    }
}