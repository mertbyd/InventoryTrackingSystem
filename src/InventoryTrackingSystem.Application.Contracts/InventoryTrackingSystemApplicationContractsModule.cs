using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace InventoryTrackingSystem;

[DependsOn(
    typeof(InventoryTrackingSystemDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
    )]
public class InventoryTrackingSystemApplicationContractsModule : AbpModule
{

}
