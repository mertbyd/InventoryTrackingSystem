using InventoryTrackingSystem.Entities;
using InventoryTrackingSystem.EntityFrameworkCore;
using InventoryTrackingSystem.Interface;
using Volo.Abp.EntityFrameworkCore;

namespace InventoryTrackingSystem.Repository;

public class MovementRequestResponseRepository : BaseRepository<MovementRequestResponse>, IMovementRequestResponseRepository
{
    public MovementRequestResponseRepository(IDbContextProvider<InventoryTrackingSystemDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }
}