using Localization.Resources.AbpUi;
using InventoryTrackingSystem.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace InventoryTrackingSystem;

[DependsOn(
    typeof(InventoryTrackingSystemApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class InventoryTrackingSystemHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(InventoryTrackingSystemHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<InventoryTrackingSystemResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}
