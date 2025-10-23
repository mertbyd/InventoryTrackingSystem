using InventoryTrackingSystem.Entities;
using InventoryTrackingSystem.EntityFrameworkCore;
using InventoryTrackingSystem.Interface;
using Volo.Abp.EntityFrameworkCore;

namespace InventoryTrackingSystem.Repository;

public class StockRepository : BaseRepository<Stock>, IStockRepository
{
    public StockRepository(IDbContextProvider<InventoryTrackingSystemDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }
}