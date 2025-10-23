using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace InventoryTrackingSystem.MongoDB;

[ConnectionStringName(InventoryTrackingSystemDbProperties.ConnectionStringName)]
public interface IInventoryTrackingSystemMongoDbContext : IAbpMongoDbContext
{
    /* Define mongo collections here. Example:
     * IMongoCollection<Question> Questions { get; }
     */
}
