using Volo.Abp;
using Volo.Abp.MongoDB;

namespace InventoryTrackingSystem.MongoDB;

public static class InventoryTrackingSystemMongoDbContextExtensions
{
    public static void ConfigureInventoryTrackingSystem(
        this IMongoModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));
    }
}
