using Domain.Entities.Common;

namespace Application.Repositories;

public interface IWriteRepository<T> : IRepository<T> where T : BaseEntity, new()
{
    void Add(T entity);
    Task AddAsync(T entity);
    void AddRange(IEnumerable<T> entities);
    Task AddRangeAsync(IEnumerable<T> entities);
    void Update(T entity);
    void UpdateRange(IEnumerable<T> entities);
    void Remove(T entity, bool hardDelete = false);
    void RemoveRange(List<T> entities, bool hardDelete = false);
    int Save();
    Task<int> SaveAsync();
}