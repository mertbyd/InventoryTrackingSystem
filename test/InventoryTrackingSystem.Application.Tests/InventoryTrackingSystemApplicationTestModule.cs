using Volo.Abp.Modularity;

namespace InventoryTrackingSystem;

[DependsOn(
    typeof(InventoryTrackingSystemApplicationModule),
    typeof(InventoryTrackingSystemDomainTestModule)
    )]
public class InventoryTrackingSystemApplicationTestModule : AbpModule
{

}
