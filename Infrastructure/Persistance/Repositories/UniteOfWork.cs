using Domain.Interfaces;
using Persistance.Data;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Repositories
{
    public class UniteOfWork : IUniteOfWork
    { 
        private readonly StoreContext _storeContext;
        private readonly ConcurrentDictionary<string, object> _repositories;

        public UniteOfWork(StoreContext storeContext, ConcurrentDictionary<string, object> repositories)
        {
            _storeContext = storeContext;
            _repositories = repositories;
        }

        public async Task<int> SaveChangesAsync() => await _storeContext.SaveChangesAsync();
        public IGenaricRepository<TEntity, Tkey> GetRepository<TEntity, Tkey>() where TEntity : BaseEntity<Tkey>
         => (IGenaricRepository<TEntity, Tkey>)_repositories
            .GetOrAdd(typeof(TEntity).Name, _ => new GenaricRepository<TEntity, Tkey>(_storeContext));

    }
}
