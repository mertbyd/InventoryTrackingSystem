using Volo.Abp.AspNetCore.Components.Server.Theming;
using Volo.Abp.Modularity;

namespace InventoryTrackingSystem.Blazor.Server;

[DependsOn(
    typeof(AbpAspNetCoreComponentsServerThemingModule),
    typeof(InventoryTrackingSystemBlazorModule)
    )]
public class InventoryTrackingSystemBlazorServerModule : AbpModule
{

}
