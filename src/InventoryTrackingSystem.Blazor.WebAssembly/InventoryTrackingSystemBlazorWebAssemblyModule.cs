using Volo.Abp.AspNetCore.Components.WebAssembly.Theming;
using Volo.Abp.Modularity;

namespace InventoryTrackingSystem.Blazor.WebAssembly;

[DependsOn(
    typeof(InventoryTrackingSystemBlazorModule),
    typeof(InventoryTrackingSystemHttpApiClientModule),
    typeof(AbpAspNetCoreComponentsWebAssemblyThemingModule)
    )]
public class InventoryTrackingSystemBlazorWebAssemblyModule : AbpModule
{

}
