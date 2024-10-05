using Domain.Interfaces;
using Domain.Models;
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


        public ProductWithBrandAndTypeSpcefications(string? sort,int? brandId,int? typeId) :
           base(product => 
           (!brandId.HasValue || product.BrandId == brandId) &&
           (!typeId.HasValue || product.TypeId == typeId))
        {
            AddInclude(product => product.Brand);
            AddInclude(product => product.Type);

            if (!string.IsNullOrWhiteSpace(sort))
            {
                switch (sort.ToLower().Trim())
                {
                    case "pricedesc":
                        SetOrderByDescinding(p=>p.Price);
                        break;
                    case "priceasc":
                        SetOrderBy(p => p.Price);
                        break;
                    case "namedecs":
                        SetOrderByDescinding(p => p.Name);
                        break;
                    default :
                        SetOrderBy(p => p.Name);
                        break;
                }
            }
        }
    }
}
