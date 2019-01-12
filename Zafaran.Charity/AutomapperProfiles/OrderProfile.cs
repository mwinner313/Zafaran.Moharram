using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Zafaran.Charity.Models;
using Zafaran.Charity.ViewModels;

namespace Zafaran.Charity.AutomapperProfiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderAddOrUpdateModel, Order>();
            CreateMap<OrderItemAddOrUpdateModel, OrderItem>();
            CreateMap<Order, OrderViewModel>();
            CreateMap<OrderItem, OrderItemViewModel>();
        }
    }

}