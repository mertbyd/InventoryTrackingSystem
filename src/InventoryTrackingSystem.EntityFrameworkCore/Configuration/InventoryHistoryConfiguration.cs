using InventoryTrackingSystem.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryTrackingSystem.Configuration;

public class InventoryHistoryConfiguration:IEntityTypeConfiguration<InventoryHistory>
{
    public void Configure(EntityTypeBuilder<InventoryHistory> builder)
    {
        builder.ToTable(InventoryTrackingSystemDbProperties.DbTablePrefix + "InventoryHistories",
            InventoryTrackingSystemDbProperties.DbSchema);
        builder.Property(x => x.InventoryId)
            .IsRequired();
        builder.Property(x => x.EventType)
            .IsRequired()
            .HasMaxLength(100);
        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(500);
        // Foreign key ilişkileri
        builder.HasOne<Inventory>()
            .WithMany()
            .HasForeignKey(x => x.InventoryId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasOne<InventoryMovement>()
            .WithMany()
            .HasForeignKey(x => x.MovementId)
            .OnDelete(DeleteBehavior.SetNull);
        // Index"ler
        builder.HasIndex(x => x.InventoryId);
        builder.HasIndex(x => x.EventType);
        builder.HasIndex(x => x.MovementId);
        builder.HasIndex(x => x.UserId);
    }
}