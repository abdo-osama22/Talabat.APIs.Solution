using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications.Specifications
{
    public interface ISpecifications <T> where T : BaseEntity
    {
        //ToDo prop for Where Condation
         Expression<Func<T,bool>> Cirteria { get; set; }

        //ToDo prop for Lis of Includes 
        List<Expression<Func<T, object>>> Includes { get; set; }

        //ToDo prop for [OrderBy( p => p.Names) ]
         Expression<Func<T,object> > OrderBy { get; set; }

        //ToDo prop for [OrderByDesc( p => p.Names) ]
        Expression<Func<T,object> > OrderByDesc { get; set; }

        int Take { get; set; }
        int Skip { get; set; }

        bool IsPaginationEnable { get; set; }
    }
}
