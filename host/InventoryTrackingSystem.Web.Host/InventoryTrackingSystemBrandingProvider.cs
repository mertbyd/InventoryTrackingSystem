using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace InventoryTrackingSystem;

[Dependency(ReplaceServices = true)]
public class InventoryTrackingSystemBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "InventoryTrackingSystem";
}
