using AutoMapper;
using InventoryApi.Entities;
using InventoryApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryApi.Helpers
{
    public class MapProfiler : Profile
    {
        public MapProfiler()
        {
            CreateMap<Shelf, ShelfVM>()
                .ForMember(pVM => pVM.Url, p => p.ResolveUsing<GetSingleResourceUrlResolver<Shelf, ShelfVM>>());

            CreateMap<ProductActivity, ProductActivityVM>();

            CreateMap<Product, ProductVM>()
                .ForMember(pVM => pVM.ShelfCode, p => p.MapFrom(src => src.Shelf.ShelfCode))
                .ForMember(pVM => pVM.Url, p => p.ResolveUsing<GetSingleResourceUrlResolver<Product, ProductVM>>())
                .ForMember(pVM => pVM.ProductActivities, p => p.MapFrom(s => s.ProductActivities.ToList()));
                        
        }
    }
}
