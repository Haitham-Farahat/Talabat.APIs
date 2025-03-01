﻿using AutoMapper;
using Talabat.APIs.DTOs;
using Talabat.Core.Entites;

namespace Talabat.APIs.Helpers
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(d=>d.ProductType , O=>O.MapFrom(S=>S.ProductType.Name))
                .ForMember(d=>d.ProductBrand , O=>O.MapFrom(S=>S.ProductBrand.Name))
                .ForMember(d=>d.PictureUrl , O=>O.MapFrom<ProductsPictureUrlResolver>());
        }
    }
}
