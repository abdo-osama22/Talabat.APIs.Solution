using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repository;
using Talabat.Core.Specifications.Specifications;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _store;

        public GenericRepository(StoreContext store)
        {
            _store = store;
        }

        #region Without Specification
        public async Task<IReadOnlyList<T>> GetAllAsync()
          => await _store.Set<T>().ToListAsync();
        

       

        public async Task<T> GetAsync(int id)
        {
            // return await _store.Set<T>().Where( x => x.Id == id).FirstOrDefaultAsync(); // Not Local

            return await _store.Set<T>().FindAsync(id); // Local

        }

        #endregion


        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecifications<T> Spec)
        =>  await ApplySpecification(Spec).ToListAsync();

        
        public async Task<T> GetWithSpecAsync(ISpecifications<T> Spec)
        => await ApplySpecification(Spec).FirstOrDefaultAsync();
        


        private IQueryable<T> ApplySpecification(ISpecifications<T> Spec)
        => SpecificationsEvalutor<T>.GetQuery(_store.Set<T>(), Spec);

        public async Task<int> GetCountWithSpecAsync(ISpecifications<T> Spec)
        => await ApplySpecification(Spec).CountAsync();

        public async Task AddAsync(T item)
        => await _store.Set<T>().AddAsync(item);
    }
}
