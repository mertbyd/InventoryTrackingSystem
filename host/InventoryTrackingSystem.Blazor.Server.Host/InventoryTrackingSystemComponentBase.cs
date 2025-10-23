using InventoryTrackingSystem.Localization;
using Volo.Abp.AspNetCore.Components;

namespace InventoryTrackingSystem.Blazor.Server.Host;

public abstract class InventoryTrackingSystemComponentBase : AbpComponentBase
{
    protected InventoryTrackingSystemComponentBase()
    {
        LocalizationResource = typeof(InventoryTrackingSystemResource);
    }
}
