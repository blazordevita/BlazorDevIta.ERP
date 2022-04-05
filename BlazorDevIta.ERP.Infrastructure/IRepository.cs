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

        Task Create(TEntity entity);
        Task Update(TEntity entity);
        Task Delete(TKey id);
    }
}
