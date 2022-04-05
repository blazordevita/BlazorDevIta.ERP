using Microsoft.EntityFrameworkCore;

namespace BlazorDevIta.ERP.Infrastructure.EF
{
    public class EFRepository<TEntity, TKey>
        : IRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>, new()
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<TEntity> _set;

        public EFRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _set = _dbContext.Set<TEntity>();
        }

        public IQueryable<TEntity> GetAll()
        {
            return _set.AsNoTracking();
        }

        public async Task<TEntity?> GetByIdAsync(TKey id)
        {
            var entity = await _set.FindAsync(id);
            if (entity == null) return null;
            _dbContext.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        public async Task Create(TEntity entity)
        {
            _set.Add(entity);
           await _dbContext.SaveChangesAsync();
           _dbContext.Entry(entity).State = EntityState.Detached;
        }

        public async Task Update(TEntity entity)
        {
            _set.Update(entity);
            await _dbContext.SaveChangesAsync();
            _dbContext.Entry(entity).State = EntityState.Detached;
        }
        public Task Delete(TKey id)
        {
            var entity = new TEntity()
            {
                Id = id,
            };
            _set.Remove(entity);
            return _dbContext.SaveChangesAsync(); 
        }
    }
}