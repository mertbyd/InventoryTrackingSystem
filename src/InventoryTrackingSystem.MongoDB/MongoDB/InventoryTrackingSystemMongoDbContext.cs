using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace InventoryTrackingSystem.MongoDB;

[ConnectionStringName(InventoryTrackingSystemDbProperties.ConnectionStringName)]
public class InventoryTrackingSystemMongoDbContext : AbpMongoDbContext, IInventoryTrackingSystemMongoDbContext
{
    /* Add mongo collections here. Example:
     * public IMongoCollection<Question> Questions => Collection<Question>();
     */

    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);

        modelBuilder.ConfigureInventoryTrackingSystem();
    }
}
