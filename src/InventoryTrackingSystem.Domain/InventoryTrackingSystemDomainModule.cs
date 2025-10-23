using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace InventoryTrackingSystem;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(InventoryTrackingSystemDomainSharedModule)
)]
public class InventoryTrackingSystemDomainModule : AbpModule
{

}
