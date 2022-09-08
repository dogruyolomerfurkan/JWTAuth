using Domain.Entities.Common;
using System.Linq.Expressions;

namespace Application.Repositories;

public interface IReadRepository<T> : IRepository<T> where T : BaseEntity, new()
{
    /// <summary>
    ///     Returns Queryable result which is evaluate later, adds filtering if its needed
    /// </summary>
    IQueryable<T> Get(Expression<Func<T, bool>> filter, bool tracking = true, params Expression<Func<T, object>>[] includes);

    /// <summary>
    ///     Get item from database, add includes if its needed
    /// </summary>
    T GetById(int id, bool tracking = true, params Expression<Func<T, object>>[] includes);
    Task<T> GetByIdAsync(int id, bool tracking = true, params Expression<Func<T, object>>[] includes);
    bool Any(Expression<Func<T, bool>> filter, bool tracking = true);
    Task<bool> AnyAsync(Expression<Func<T, bool>> filter, bool tracking = true);
    int Count(Expression<Func<T, bool>> filter, bool tracking = true);
    Task<int> CountAsync(Expression<Func<T, bool>> filter, bool tracking = true);
}