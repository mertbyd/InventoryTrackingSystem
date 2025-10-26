using System;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Services;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;

namespace InventoryTrackingSystem.Manager;

public abstract class BaseManager<TEntity> : DomainService 
    where TEntity : class, IEntity<Guid>
{
    protected readonly IRepository<TEntity, Guid> Repository;
    protected BaseManager(IRepository<TEntity, Guid> repository)
    {
        Repository = repository;
    }
    // Kendi entity'si için varlık kontrolü   -- verilen id de bir kayıy varmı ona bakar
    protected async Task ValidateEntityExistsAsync(Guid id, string errorCode)
    {
        var entity = await Repository.FindAsync(id);
        if (entity == null)
            throw new BusinessException(errorCode);
    }
    // Farklı entity'ler için varlık kontrolü (lookup tablolar için)
    protected async Task ValidateLookupExistsAsync<TLookup>(
        IRepository<TLookup, Guid> lookupRepository, 
        Guid id, 
        string errorCode) where TLookup : class, IEntity<Guid>
    {
        var entity = await lookupRepository.FindAsync(id);
        if (entity == null)
        {
            throw new BusinessException(errorCode);
        }
    }

    protected async Task<TEntity> GetEntityAsync(Guid id, string errorCode)
    {
        var entity = await Repository.GetAsync(id);
        if (entity == null)
            throw new BusinessException(errorCode);
        return entity;
    }
    

}