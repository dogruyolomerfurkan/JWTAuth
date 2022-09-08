using Application.Repositories;
using Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity, new()
{
    private readonly DbSet<T> _entity;
    private readonly AuthDbContext _dbContext;

    public DbSet<T> Entity => _entity;

    public WriteRepository(AuthDbContext dbContext)
    {
        _dbContext = dbContext;
        _entity = dbContext.Set<T>();
    }

    public void Add(T entity)
    {
        _entity.Add(entity);
    }

    public async Task AddAsync(T entity)
    {
        await _entity.AddAsync(entity);
    }

    public void AddRange(IEnumerable<T> entities)
    {
        _entity.AddRange(entities);

    }

    public async Task AddRangeAsync(IEnumerable<T> entities)
    {
        await _entity.AddRangeAsync(entities);
    }

    public void Update(T entity)
    {
        _entity.Attach(entity);
    }

    public void UpdateRange(IEnumerable<T> entities)
    {
        _entity.AttachRange(entities);
    }

    public void Remove(T entity, bool hardDelete = false)
    {
        if (hardDelete)
        {
            _entity.Remove(entity);
        }
        else
        {
            _entity.Attach(entity);
            entity.IsDeleted = true;
        }
    }

    public void RemoveRange(List<T> entities, bool hardDelete = false)
    {
        if (hardDelete)
        {
            _entity.RemoveRange(entities);
        }
        else
        {
            _entity.AttachRange(entities);
            entities.ForEach(entity => entity.IsDeleted = true);
        }
    }

    public int Save() => _dbContext.SaveChanges();

    public async Task<int> SaveAsync() => await _dbContext.SaveChangesAsync();

}