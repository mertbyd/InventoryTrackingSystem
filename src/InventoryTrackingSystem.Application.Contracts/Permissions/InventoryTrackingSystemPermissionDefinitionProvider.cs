using InventoryTrackingSystem.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace InventoryTrackingSystem.Permissions;

public class InventoryTrackingSystemPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(InventoryTrackingSystemPermissions.GroupName, L("Permission:InventoryTrackingSystem"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<InventoryTrackingSystemResource>(name);
    }
}
