using InventoryTrackingSystem.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace InventoryTrackingSystem.Pages;

public abstract class InventoryTrackingSystemPageModel : AbpPageModel
{
    protected InventoryTrackingSystemPageModel()
    {
        LocalizationResourceType = typeof(InventoryTrackingSystemResource);
    }
}
