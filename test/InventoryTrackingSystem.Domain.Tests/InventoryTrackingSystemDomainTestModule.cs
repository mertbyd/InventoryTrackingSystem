using InventoryTrackingSystem.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace InventoryTrackingSystem;

/* Domain tests are configured to use the EF Core provider.
 * You can switch to MongoDB, however your domain tests should be
 * database independent anyway.
 */
[DependsOn(
    typeof(InventoryTrackingSystemEntityFrameworkCoreTestModule)
    )]
public class InventoryTrackingSystemDomainTestModule : AbpModule
{

}
