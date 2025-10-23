using InventoryTrackingSystem.Entities;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace InventoryTrackingSystem.EntityFrameworkCore;

[ConnectionStringName(InventoryTrackingSystemDbProperties.ConnectionStringName)]
public interface IInventoryTrackingSystemDbContext : IEfCoreDbContext
{
  // Lookups
  DbSet<Site> Sites { get; set; }
  DbSet<Vehicle> Vehicles { get; set; }
  DbSet<UnavailabilityReason> UnavailabilityReasons { get; set; }
  DbSet<SerialNumber> SerialNumbers { get; set; } // YENİ
    
  // Domain
  DbSet<Inventory> Inventories { get; set; }
  DbSet<MovementRequest> MovementRequests { get; set; }
  DbSet<MovementRequestDetail> MovementRequestDetails { get; set; }
  DbSet<MovementRequestResponse> MovementRequestResponses { get; set; }
  DbSet<InventoryMovement> InventoryMovements { get; set; }
  DbSet<Stock> Stocks { get; set; } 
    
  // Audit
  DbSet<InventoryHistory> InventoryHistories { get; set; }
  
  

}
