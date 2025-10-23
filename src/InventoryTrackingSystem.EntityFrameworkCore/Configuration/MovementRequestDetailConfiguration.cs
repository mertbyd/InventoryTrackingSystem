using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using InventoryTrackingSystem.Entities;

namespace InventoryTrackingSystem.EntityFrameworkCore.Configurations;

public class MovementRequestDetailConfiguration : IEntityTypeConfiguration<MovementRequestDetail>
{
    public void Configure(EntityTypeBuilder<MovementRequestDetail> builder)
    {
        builder.ToTable(InventoryTrackingSystemDbProperties.DbTablePrefix + "MovementRequestDetails",
            InventoryTrackingSystemDbProperties.DbSchema);
        builder.Property(x => x.MovementRequestId)
            .IsRequired();
        builder.Property(x => x.RequestedSerialNumberId)
            .IsRequired();
        builder.Property(x => x.RequestedQuantity)
            .IsRequired();
        // Foreign key ilişkileri
        builder.HasOne<MovementRequest>()
            .WithMany()
            .HasForeignKey(x => x.MovementRequestId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasOne<SerialNumber>()
            .WithMany()
            .HasForeignKey(x => x.RequestedSerialNumberId)
            .OnDelete(DeleteBehavior.Restrict);
        // Index"ler
        builder.HasIndex(x => x.MovementRequestId);
        builder.HasIndex(x => x.RequestedSerialNumberId);
    }
}