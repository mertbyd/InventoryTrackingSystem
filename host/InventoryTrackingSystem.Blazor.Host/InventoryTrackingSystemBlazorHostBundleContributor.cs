using Volo.Abp.Bundling;

namespace InventoryTrackingSystem.Blazor.Host;

public class InventoryTrackingSystemBlazorHostBundleContributor : IBundleContributor
{
    public void AddScripts(BundleContext context)
    {

    }

    public void AddStyles(BundleContext context)
    {
        context.Add("main.css", true);
    }
}
