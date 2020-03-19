using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using AutoMapper;
using Infrastructure.Entities;
using Domain.DTO;

namespace Domain.Mappers
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap()
            //.ForMember(p => p.Address, d => d.MapFrom(c => c.Address))
            //.ForMember(p => p.Id, d => d.MapFrom(c => c.Id))
            //.ForMember(p => p.Phone, d => d.MapFrom(c => c.Phone))
            //.ForMember(p => p.Orders, d => d.MapFrom(c => c.Orders))
            ;
            CreateMap<Order, OrderDTO>()
            .ForMember(dst => dst.Product,
             opt => opt.MapFrom(o => o.OrderProduct.Select(i => i.Product).ToList())).ReverseMap();
            CreateMap<Product, ProductDTO>()
            .ForMember(dst => dst.Order,
             opt => opt.MapFrom(o => o.OrderProduct.Select(i => i.Order).ToList())).ReverseMap();
        }
    }
}
