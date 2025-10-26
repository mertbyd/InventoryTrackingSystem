using System.Collections.Generic;
using InventoryTrackingSystem.Dtos.Stock;
using Volo.Abp.Application.Services;
using System.Threading.Tasks;
namespace InventoryTrackingSystem.İnterface;

public  interface IStockAppService:IApplicationService
{
    Task<List<StockDto>> GetStockListAsync();
}