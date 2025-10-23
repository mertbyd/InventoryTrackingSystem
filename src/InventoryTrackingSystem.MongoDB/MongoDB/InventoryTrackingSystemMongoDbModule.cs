using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using Volo.Abp.MongoDB;

namespace InventoryTrackingSystem.MongoDB;

[DependsOn(
    typeof(InventoryTrackingSystemDomainModule),
    typeof(AbpMongoDbModule)
    )]
public class InventoryTrackingSystemMongoDbModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddMongoDbContext<InventoryTrackingSystemMongoDbContext>(options =>
        {
                /* Add custom repositories here. Example:
                 * options.AddRepository<Question, MongoQuestionRepository>();
                 */
        });
    }
}
