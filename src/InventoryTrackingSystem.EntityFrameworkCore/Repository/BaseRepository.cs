using System;
using InventoryTrackingSystem.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using InventoryTrackingSystem.EntityFrameworkCore; 
namespace InventoryTrackingSystem.Repository;

public class BaseRepository<T>:EfCoreRepository<InventoryTrackingSystemDbContext,T,Guid> where T:class,IEntity<Guid>
{
    public BaseRepository(IDbContextProvider<InventoryTrackingSystemDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }
}