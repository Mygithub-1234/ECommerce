﻿using AutoMapper;

namespace ECommerce.API.Customers.Profiles
{
    public class CustomerProfile :AutoMapper.Profile
    {
        public CustomerProfile()
        {
            CreateMap<DB.Customer, Models.Customer>();
        }
    }
}
