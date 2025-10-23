using InventoryTrackingSystem.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace InventoryTrackingSystem.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class InventoryTrackingSystemPageModel : AbpPageModel
{
    protected InventoryTrackingSystemPageModel()
    {
        LocalizationResourceType = typeof(InventoryTrackingSystemResource);
        ObjectMapperContext = typeof(InventoryTrackingSystemWebModule);
    }
}
