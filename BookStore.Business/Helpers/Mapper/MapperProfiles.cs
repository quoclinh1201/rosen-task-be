using AutoMapper;
using BookStore.Business.Dto.RequestObjects;
using BookStore.Business.Dto.ResponseObjects;
using BookStore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Data.Helpers.Mapper
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles() 
        { 
            CreateMap<CreateAccountRequest, Account>()
                .ForMember(d => d.Role, s => s.MapFrom(s => "Customer"))
                .ForMember(d => d.CreateAt, s => s.MapFrom(s => DateTime.UtcNow.AddHours(7)))
                .ForMember(d => d.IsActive, s => s.MapFrom(s => true));

            CreateMap<CreateAccountRequest, User>()
                .ForMember(d => d.IsActive, s => s.MapFrom(s => true));

            CreateMap<Product, GetListProductResponse>()
                .ForMember(d => d.Price, s => s.MapFrom(s => s.Price.ToString().Substring(0, s.Price.ToString().Length - 4)));

            CreateMap<Product, ProductDetailResponse>()
                .ForMember(d => d.ProductImages, s => s.MapFrom(s => s.ProductImages.Select(ss => ss.ImageUrl).ToList()))
                .ForMember(d => d.Category, s => s.MapFrom(s => s.Category.CategoryName));
        }
    }
}
