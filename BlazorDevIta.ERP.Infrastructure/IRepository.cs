using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorDevIta.ERP.Infrastructure
{
    public interface IRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>, new()
    {
        IQueryable<TEntity> GetAll();
        Task<TEntity?> GetByIdAsync(TKey id);

        Task CreateAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TKey id);
    }
}
