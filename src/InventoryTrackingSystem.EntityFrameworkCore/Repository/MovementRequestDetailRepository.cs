using InventoryTrackingSystem.Entities;
using InventoryTrackingSystem.EntityFrameworkCore;
using InventoryTrackingSystem.Interface;
using Volo.Abp.EntityFrameworkCore;

namespace InventoryTrackingSystem.Repository;

public class MovementRequestDetailRepository : BaseRepository<MovementRequestDetail>, IMovementRequestDetailRepository
{
    public MovementRequestDetailRepository(IDbContextProvider<InventoryTrackingSystemDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }
}