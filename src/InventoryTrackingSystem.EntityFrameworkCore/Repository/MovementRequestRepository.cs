using InventoryTrackingSystem.Entities;
using InventoryTrackingSystem.EntityFrameworkCore;
using InventoryTrackingSystem.Interface;
using Volo.Abp.EntityFrameworkCore;

namespace InventoryTrackingSystem.Repository;

public class MovementRequestRepository : BaseRepository<MovementRequest>, IMovementRequestRepository
{
    public MovementRequestRepository(IDbContextProvider<InventoryTrackingSystemDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }
}