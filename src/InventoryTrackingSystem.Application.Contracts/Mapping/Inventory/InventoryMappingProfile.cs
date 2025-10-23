using AutoMapper;
using  InventoryTrackingSystem.Dtos.Inventory;
using InventoryTrackingSystem.Models.Inventories;
using InventoryTrackingSystem.Entities;
namespace InventoryTrackingSystem.Mapping.Inventory;

public class InventoryMappingProfile:Profile
{
    public InventoryMappingProfile()
    {
        CreateMap<CreateInventoryDto, CreateInventoryModel>();
        CreateMap<UpdateInventoryDto, UpdateInventoryModel>();
        CreateMap<UpdateInventoryDto, Entities.Inventory>();
    }
}