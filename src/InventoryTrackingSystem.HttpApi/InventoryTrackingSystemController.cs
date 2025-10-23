using InventoryTrackingSystem.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace InventoryTrackingSystem;

public abstract class InventoryTrackingSystemController : AbpControllerBase
{
    protected InventoryTrackingSystemController()
    {
        LocalizationResource = typeof(InventoryTrackingSystemResource);
    }
}
