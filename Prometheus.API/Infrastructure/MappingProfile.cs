﻿using AutoMapper;
using Prometheus.DB.Entities;
using Prometheus.Model.Apartment;
using Prometheus.Model.Bill;
using Prometheus.Model.User;

namespace Prometheus.API.Infrastructure
{
    public class MappingProfile : Profile
    {
        // mapping db classes with viewmodels
        public MappingProfile()
        {
            CreateMap<User, UserViewModel>();
            CreateMap<UserViewModel, User>();

            CreateMap<User, AddUserViewModel>();
            CreateMap<AddUserViewModel, User>();

            CreateMap<Bill, BillViewModel>();
            CreateMap<BillViewModel, Bill>();

            CreateMap<Bill, AddBillViewModel>();
            CreateMap<AddBillViewModel, Bill>();

            CreateMap<Apartment, ApartmentViewModel>();
            CreateMap<ApartmentViewModel, Apartment>();
        }
    }
}
