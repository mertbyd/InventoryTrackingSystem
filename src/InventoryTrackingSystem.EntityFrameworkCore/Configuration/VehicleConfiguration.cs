using System;
using InventoryTrackingSystem.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryTrackingSystem.EntityFrameworkCore.Configurations;

public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        builder.ToTable(InventoryTrackingSystemDbProperties.DbTablePrefix + "Vehicles",
            InventoryTrackingSystemDbProperties.DbSchema);
        
        builder.Property(x => x.PlateNumber)
            .IsRequired()
            .HasMaxLength(20);
        
        builder.Property(x => x.Type)
            .IsRequired()
            .HasMaxLength(30);
        
        // Plaka numarası unique olmalı
        builder.HasIndex(x => x.PlateNumber).IsUnique();
        
        // Type için index (filtreleme için)
        builder.HasIndex(x => x.Type);
    }
}