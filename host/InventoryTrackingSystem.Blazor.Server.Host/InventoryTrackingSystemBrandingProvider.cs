using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace InventoryTrackingSystem.Blazor.Server.Host;

[Dependency(ReplaceServices = true)]
public class InventoryTrackingSystemBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "InventoryTrackingSystem";
}
