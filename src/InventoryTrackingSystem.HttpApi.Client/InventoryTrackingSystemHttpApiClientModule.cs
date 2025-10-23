using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace InventoryTrackingSystem;

[DependsOn(
    typeof(InventoryTrackingSystemApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class InventoryTrackingSystemHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(InventoryTrackingSystemApplicationContractsModule).Assembly,
            InventoryTrackingSystemRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<InventoryTrackingSystemHttpApiClientModule>();
        });

    }
}
