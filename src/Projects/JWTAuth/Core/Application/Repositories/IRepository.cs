using Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace Application.Repositories;

public interface IRepository<T> where T : BaseEntity, new()
{
    DbSet<T> Entity { get; }
}