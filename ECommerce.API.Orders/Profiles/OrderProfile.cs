﻿using AutoMapper;

namespace ECommerce.API.Orders.Profiles
{
    public class OrderProfile : AutoMapper.Profile
    {
        public OrderProfile()
        {
            CreateMap<DB.Order, Models.Order>();
            CreateMap<DB.OrderItem, Models.OrderItem>();
        }
    }
}
