using Application.Repositories;
using Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using System.Linq.Expressions;

namespace Persistence.Repositories;

public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity, new()
{
    private readonly DbSet<T> _entity;

    public DbSet<T> Entity => _entity;

    public ReadRepository(AuthDbContext dbContext)
    {
        _entity = dbContext.Set<T>();
    }

    public IQueryable<T> Get(Expression<Func<T, bool>> filter, bool tracking = true, params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _entity.Where(filter).AsQueryable();
        if (!tracking) query.AsNoTracking();
        return query;
    }

    public T GetById(int id, bool tracking = true, params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _entity.AsQueryable();

        if (includes.Any())
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        if (!tracking) query = query.AsNoTracking();

        return query.First(p => p.Id == id);
    }

    public async Task<T> GetByIdAsync(int id, bool tracking = true, params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _entity.AsQueryable();

        if (includes.Any())
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        if (!tracking) query = query.AsNoTracking();

        return await query.FirstAsync(p => p.Id == id);
    }

    public int Count(Expression<Func<T, bool>> filter, bool tracking = true)
    {
        IQueryable<T> query = _entity.Where(filter).AsQueryable();
        if (!tracking) query = query.AsNoTracking();
        return query.Count();

    }

    public async Task<int> CountAsync(Expression<Func<T, bool>> filter, bool tracking = true)
    {
        IQueryable<T> query = _entity.Where(filter).AsQueryable();
        if (!tracking) query = query.AsNoTracking();
        return await query.CountAsync();
    }
    public bool Any(Expression<Func<T, bool>> filter, bool tracking = true)
    {
        IQueryable<T> query = _entity.Where(filter).AsQueryable();
        if (!tracking) query = query.AsNoTracking();
        return query.Any();
    }

    public async Task<bool> AnyAsync(Expression<Func<T, bool>> filter, bool tracking = true)
    {
        IQueryable<T> query = _entity.Where(filter).AsQueryable();
        if (!tracking) query = query.AsNoTracking();
        return await query.AnyAsync();
    }
}