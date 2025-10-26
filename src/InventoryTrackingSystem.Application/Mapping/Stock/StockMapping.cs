// Mapping/Stock/StockMapping.cs
using AutoMapper;
using InventoryTrackingSystem.Dtos.Stock;
using Volo.Abp.AutoMapper;

namespace InventoryTrackingSystem.Mapping.Stock;

public class StockMapping : Profile
{
    public StockMapping()
    {
        CreateMap<Entities.Stock, StockDto>();
    }
}