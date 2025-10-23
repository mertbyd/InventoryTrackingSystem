using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace InventoryTrackingSystem;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(InventoryTrackingSystemHttpApiClientModule),
    typeof(AbpHttpClientIdentityModelModule)
    )]
public class InventoryTrackingSystemConsoleApiClientModule : AbpModule
{

}
