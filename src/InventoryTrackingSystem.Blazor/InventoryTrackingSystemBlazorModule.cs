using Microsoft.Extensions.DependencyInjection;
using InventoryTrackingSystem.Blazor.Menus;
using Volo.Abp.AspNetCore.Components.Web.Theming;
using Volo.Abp.AspNetCore.Components.Web.Theming.Routing;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;

namespace InventoryTrackingSystem.Blazor;

[DependsOn(
    typeof(InventoryTrackingSystemApplicationContractsModule),
    typeof(AbpAspNetCoreComponentsWebThemingModule),
    typeof(AbpAutoMapperModule)
    )]
public class InventoryTrackingSystemBlazorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<InventoryTrackingSystemBlazorModule>();

        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddProfile<InventoryTrackingSystemBlazorAutoMapperProfile>(validate: true);
        });

        Configure<AbpNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new InventoryTrackingSystemMenuContributor());
        });

        Configure<AbpRouterOptions>(options =>
        {
            options.AdditionalAssemblies.Add(typeof(InventoryTrackingSystemBlazorModule).Assembly);
        });
    }
}
