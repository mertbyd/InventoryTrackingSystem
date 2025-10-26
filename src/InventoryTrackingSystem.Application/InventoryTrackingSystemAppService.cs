using InventoryTrackingSystem.Localization;
using Volo.Abp.Application.Services;

namespace InventoryTrackingSystem;

public abstract class InventoryTrackingSystemAppService : ApplicationService
{
    protected InventoryTrackingSystemAppService()
    {
        LocalizationResource = typeof(InventoryTrackingSystemResource);
        ObjectMapperContext = typeof(InventoryTrackingSystemApplicationModule);
    }

}
