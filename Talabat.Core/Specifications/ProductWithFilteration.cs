using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications.Specifications;

namespace Talabat.Core.Specifications
{
    public class ProductWithFiltrationForCountAsync: BaseSpecifications<Product>
    {

        public ProductWithFiltrationForCountAsync(ProductSpecParams specParams)
            : base(p => (!specParams.BrandId.HasValue || p.ProductBrandId == specParams.BrandId) &&
                         (!specParams.TypeId.HasValue || p.ProductTypeId == specParams.TypeId)
                  )
        {
            
        }
    }
}
