using AutoMapper;
using BookStore.Business.Dto.RequestObjects;
using BookStore.Business.Dto.ResponseObjects;
using BookStore.Business.Helpers.Common;
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
                .ForMember(d => d.IsAvailable, s => s.MapFrom(s => s.Quantity > 0 ? true : false))
                .ForMember(d => d.Price, s => s.MapFrom(s => FormatMoney.FormatPrice(s.Price)));

            CreateMap<Product, ProductDetailResponse>()
                .ForMember(d => d.IsAvailable, s => s.MapFrom(s => s.Quantity > 0 ? true : false))
                .ForMember(d => d.Price, s => s.MapFrom(s => FormatMoney.FormatPrice(s.Price)))
                .ForMember(d => d.Category, s => s.MapFrom(s => s.Category.CategoryName));

            CreateMap<ProductImage, ProductImageResponse>();

            CreateMap<CartDetail, CartItemResponse>();

            CreateMap<DeliveryInformation, DeliveryInformaionResponse>();

            CreateMap<CreateDeliveryInformationRequest, DeliveryInformation>()
                 .ForMember(d => d.IsActive, s => s.MapFrom(s => true));

            CreateMap<Order, GetListOrderResponse>()
                .ForMember(d => d.CreateDate, s => s.MapFrom(s => ConvertDateTime.ConvertDateTimeToString(s.CreateDate)))
                .ForMember(d => d.TotalPrice, s => s.MapFrom(s => FormatMoney.FormatPrice(s.TotalPrice)))
                .ForMember(d => d.Status, s => s.MapFrom(s => s.Status == 1 ? "Đã đặt" : "Đã hủy"));

            CreateMap<CreateOrderRequest, Order>()
                .ForMember(d => d.DeliveryId, s => s.MapFrom(s => s.DeliveryInformation))
                .ForMember(d => d.CreateDate, s => s.MapFrom(s => DateTime.UtcNow.AddHours(7)))
                .ForMember(d => d.Status, s => s.MapFrom(s => 1));

            CreateMap<Order, OrderResponse>()
                .ForMember(d => d.CreateDate, s => s.MapFrom(s => ConvertDateTime.ConvertDateTimeToString(s.CreateDate)))
                .ForMember(d => d.TotalPrice, s => s.MapFrom(s => FormatMoney.FormatPrice(s.TotalPrice)))
                .ForMember(d => d.Status, s => s.MapFrom(s => s.Status == 1 ? "Đã đặt" : "Đã hủy"));

            CreateMap<User, GetOwnProfileResponse>();

            CreateMap<Category, CategoryResponse>();
        }
    }
}
