using AutoMapper;
using Domain.Models;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfiles
{
    internal class ProductProfile : Profile
    {
        public ProductProfile() 
        {
            CreateMap<Product, ProductResultDTO>()
                .ForMember(p => p.BrandName, options => options.
                MapFrom(s => s.Brand.Name))
                .ForMember(p => p.TypeName, options => options.
                MapFrom(s => s.Type.Name))
                .ForMember(p => p.PictureUrl, options => options.
                MapFrom<PictureURLResolver>());

            CreateMap<ProductBrand, ProductBrandDTO>();
            CreateMap<ProductType, ProductTypeDTO>();

        }

    }
}
