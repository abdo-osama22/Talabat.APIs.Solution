using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications.Specifications;

namespace Talabat.Core.Specifications
{
    public class ProductWithBrandAndTypeSpecifications : BaseSpecifications<Product>
    {

        public ProductWithBrandAndTypeSpecifications(ProductSpecParams Params) 
            : base( p => (!Params.BrandId.HasValue || p.ProductBrandId == Params.BrandId) &&
                         (!Params.TypeId.HasValue || p.ProductTypeId == Params.TypeId)
                  )
        {
            #region Add Include 

            Includes.Add(p => p.ProductBrand);
            Includes.Add(p => p.ProductType);
            #endregion

            #region Sort 
            if (!string.IsNullOrEmpty(Params.Sort))
            {
                switch (Params.Sort)
                {
                    case "PriceAsc":
                        AddOrderBy(p => p.Price);
                        break;

                    case "PriceDesc":
                        AddOrderByDesc(p => p.Price);
                        break;

                    default:
                        AddOrderBy(p => p.Name);
                        break;

                }
            }
            #endregion

            #region Pagination

            //100 Elements 
            //Page Size = 10 // Page index = 5
            // 10 * 4 // 10
            // Skip 10 // Take 10

            ApplyPagination(Params.PageSize * (Params.PageIndex - 1), Params.PageSize); 
            #endregion

        }

        public ProductWithBrandAndTypeSpecifications(Expression<Func<Product,bool>> expression) : base(expression)
        {
            Includes.Add(p => p.ProductBrand);
            Includes.Add(p => p.ProductType);
        }

        public ProductWithBrandAndTypeSpecifications(int id) : base(p => p.Id.Equals(id) ) // may overide  for equal
        {
            Includes.Add(p => p.ProductBrand);
            Includes.Add(p => p.ProductType);
        }

    }
}
