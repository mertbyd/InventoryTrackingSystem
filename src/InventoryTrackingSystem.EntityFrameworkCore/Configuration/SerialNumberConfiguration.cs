using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using InventoryTrackingSystem.Entities;

namespace InventoryTrackingSystem.EntityFrameworkCore.Configurations;

public class SerialNumberConfiguration : IEntityTypeConfiguration<SerialNumber>
{
    public void Configure(EntityTypeBuilder<SerialNumber> builder)
    {
        builder.ToTable(InventoryTrackingSystemDbProperties.DbTablePrefix + "SerialNumbers",
            InventoryTrackingSystemDbProperties.DbSchema);
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);
        builder.Property(x => x.SerialNumberPrefix)
            .IsRequired()
            .HasMaxLength(50);
        // SerialNumberPrefix unique olmalı
        builder.HasIndex(x => x.SerialNumberPrefix).IsUnique();
        // Name için index (arama için)
        builder.HasIndex(x => x.Name);
    }
}