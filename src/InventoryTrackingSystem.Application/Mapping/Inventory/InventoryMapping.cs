using AutoMapper;
using InventoryTrackingSystem.Dtos.Inventory;
using InventoryTrackingSystem.Models.Inventories;
using Volo.Abp.AutoMapper;
using Volo.Abp.ObjectMapping; // For extension methods like .IgnoreFullAuditedObjectProperties()

namespace InventoryTrackingSystem.Mapping.Inventory;

public class InventoryMapping : Profile
{
    public InventoryMapping()
    {

        CreateMap<CreateInventoryDto, CreateInventoryModel>();
        CreateMap<UpdateInventoryDto, UpdateInventoryModel>();
        CreateMap<ChangeAvailabilityStatusDto, ChangeAvailabilityStatusModel>();
        CreateMap<CreateInventoryModel, Entities.Inventory>()
            .IgnoreFullAuditedObjectProperties()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
        CreateMap<UpdateInventoryModel, Entities.Inventory>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .IgnoreFullAuditedObjectProperties();
        CreateMap<UpdateInventoryDto, Entities.Inventory>()
            .IgnoreFullAuditedObjectProperties()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
        // --- Entity -> DTO Mapping ---
        CreateMap<Entities.Inventory, InventoryDto>();
    }
}