using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications.Specifications
{
    public class BaseSpecifications<T> : ISpecifications<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>>? Cirteria { get ; set ; }
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();
        public Expression<Func<T, object>> OrderBy { get ; set ; }
        public Expression<Func<T, object>> OrderByDesc { get; set; }
        public int Take { get; set; }
        public int Skip { get; set; }
        public bool IsPaginationEnable { get; set; }

        public BaseSpecifications()
        {
        }



        public BaseSpecifications(Expression<Func<T, bool>> cirteria)
        {
            Cirteria = cirteria;
        }

        public void AddOrderBy(Expression<Func<T, object>> orderByexpression)
        => OrderBy = orderByexpression;

        public void AddOrderByDesc(Expression<Func<T, object>> orderByDescexpression)
        => OrderByDesc = orderByDescexpression;

        public void ApplyPagination(int skip,int take )
        {
            IsPaginationEnable = true;
            Take = take;
            Skip = skip;
        }

    }
}
