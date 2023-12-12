using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications.Specifications;

namespace Talabat.Core.Repository
{
    public interface IGenericRepository<T>  where T : BaseEntity
    {
        #region Without Specification

        Task<IReadOnlyList<T>> GetAllAsync();

        Task<T> GetAsync(int id);
        Task AddAsync(T item);
        #endregion


        #region With Specification

        Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecifications<T> Spec);

        Task<T> GetWithSpecAsync( ISpecifications<T> Spec);
        Task<int> GetCountWithSpecAsync( ISpecifications<T> Spec);
        #endregion





    }
}
