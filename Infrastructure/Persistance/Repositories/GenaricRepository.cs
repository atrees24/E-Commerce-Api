using Domain.Interfaces;
using Persistance.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Repositories
{
    public class GenaricRepository<TEntity, Tkey> : IGenaricRepository<TEntity, Tkey>
        where TEntity : BaseEntity<Tkey>
    {
        private readonly StoreContext _storeContext;

        public GenaricRepository(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        public async Task AddAsync(TEntity entity) => await _storeContext.Set<TEntity>().AddAsync(entity);

        public void Delete(TEntity entity) => _storeContext.Set<TEntity>().Remove(entity);

        public async Task<IEnumerable<TEntity>> GetAllAsync(bool trackChanges = false)
        {
            if(trackChanges)
              return await _storeContext.Set<TEntity>().ToListAsync();
            return await _storeContext.Set<TEntity>().AsNoTracking().ToListAsync();

        }

        public async Task<TEntity?> GetAsync(Tkey id) => await _storeContext.Set<TEntity>().FindAsync(id);

        public void Update(TEntity entity) => _storeContext.Set<TEntity>().Update(entity);
    }
}
