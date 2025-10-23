using InventoryTrackingSystem.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryTrackingSystem.Configuration;

public class InventoryMovementConfiguration:IEntityTypeConfiguration<InventoryMovement>
{
    public void Configure(EntityTypeBuilder<InventoryMovement> builder)
    {
        builder.ToTable(InventoryTrackingSystemDbProperties.DbTablePrefix + "InventoryMovements",
            InventoryTrackingSystemDbProperties.DbSchema);
        builder.Property(x => x.MovementRequestId)
            .IsRequired();
        builder.Property(x => x.InventoryId)
            .IsRequired();
        builder.Property(x => x.MovementRequestDetailId)
            .IsRequired();
        builder.Property(x => x.MovementStatus)
            .IsRequired()
            .HasMaxLength(50)
            .HasDefaultValue("DEPARTED");
        builder.Property(x => x.IsCompleted)
            .HasDefaultValue(false);
        // Foreign key ilişkileri
        builder.HasOne<MovementRequest>()
            .WithMany()
            .HasForeignKey(x => x.MovementRequestId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<Inventory>()
            .WithMany()
            .HasForeignKey(x => x.InventoryId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<MovementRequestDetail>()
            .WithMany()
            .HasForeignKey(x => x.MovementRequestDetailId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<Vehicle>()
            .WithMany()
            .HasForeignKey(x => x.VehicleId)
            .OnDelete(DeleteBehavior.SetNull);
        // Index"ler
        builder.HasIndex(x => x.MovementRequestId);
        builder.HasIndex(x => x.InventoryId);
        builder.HasIndex(x => x.MovementStatus);
        builder.HasIndex(x => x.IsCompleted);
    }
}