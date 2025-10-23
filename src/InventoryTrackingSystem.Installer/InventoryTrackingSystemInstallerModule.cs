using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace InventoryTrackingSystem;

[DependsOn(
    typeof(AbpVirtualFileSystemModule)
    )]
public class InventoryTrackingSystemInstallerModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<InventoryTrackingSystemInstallerModule>();
        });
    }
}
