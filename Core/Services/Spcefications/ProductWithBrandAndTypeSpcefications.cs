using Domain.Interfaces;
using Domain.Models;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Spcefications
{
    public class ProductWithBrandAndTypeSpcefications : Specifications<Product> 
    {
        public ProductWithBrandAndTypeSpcefications(int id)
            : base(product => product.Id == id)
        {
            AddInclude(product => product.Brand);
            AddInclude(product => product.Type);
        }


        public ProductWithBrandAndTypeSpcefications(ProductSpceficationParamters paramters) :
           base(product => 
           (!paramters.Brandid.HasValue || product.BrandId == paramters.Brandid) &&
           (!paramters.Typeid.HasValue || product.TypeId == paramters.Typeid) &&
           (string.IsNullOrWhiteSpace(paramters.search) || product.Name.ToLower().Contains(paramters.search.ToLower().Trim())))
        {
            AddInclude(product => product.Brand);
            AddInclude(product => product.Type);


            ApplyPagination(paramters.PageIndex, paramters.PageSize);

            if (paramters.sort is not null)
            {
                switch (paramters.sort)
                {
                    case ProductSortingOptions.PriceDesc:
                        SetOrderByDescinding(p=>p.Price);
                        break;
                    case ProductSortingOptions.PriceAsc:
                        SetOrderBy(p => p.Price);
                        break;
                    case ProductSortingOptions.NameDesc:
                        SetOrderByDescinding(p => p.Name);
                        break;
                    case ProductSortingOptions.NameAsc:
                        SetOrderBy(p => p.Name);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
