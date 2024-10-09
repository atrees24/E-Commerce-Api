using Domain.Interfaces;
using Domain.Models;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Services.Spcefications
{
    public class ProductCountSpcefications : Specifications<Product>
    {
        public ProductCountSpcefications(ProductSpceficationParamters paramters) :
        base(product =>
        (!paramters.Brandid.HasValue || product.BrandId == paramters.Brandid) &&
           (!paramters.Typeid.HasValue || product.TypeId == paramters.Typeid) &&
           (string.IsNullOrWhiteSpace(paramters.search) || product.Name.ToLower().Contains(paramters.search.ToLower().Trim())))
        {
           
        }
    }
}
