namespace InventoryTrackingSystem;

public static class InventoryTrackingSystemDbProperties
{
    public static string DbTablePrefix { get; set; } = "";

    public static string DbSchema { get; set; } = "inventory";

    public const string ConnectionStringName = "InventoryTrackingSystem";
}
