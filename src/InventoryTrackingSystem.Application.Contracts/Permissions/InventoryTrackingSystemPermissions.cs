using Volo.Abp.Reflection;

namespace InventoryTrackingSystem.Permissions;

public class InventoryTrackingSystemPermissions
{
    public const string GroupName = "InventoryTrackingSystem";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(InventoryTrackingSystemPermissions));
    }
}
