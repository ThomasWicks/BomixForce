using AutoMapper;
using AutoMapper.Configuration;
using Bomix_Force.Data.Entities;
using Bomix_Force.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bomix_Force.Util
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            #region User 
            CreateMap<UserViewModel, Person>().ReverseMap();
            CreateMap<UserViewEdit, Person>().ReverseMap();
            #endregion

            #region Order
            CreateMap<OrderViewModel, Order>().ReverseMap();
            #endregion
        }
    }
}
