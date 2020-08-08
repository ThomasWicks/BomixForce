using AutoMapper.Configuration;
using Bomix_Force.Data.Entities;
using Bomix_Force.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bomix_Force.Util
{
    public class AutoMapperConfig
    {
        public static MapperConfigurationExpression RegisterMappings()
        {
            MapperConfigurationExpression cfg = new MapperConfigurationExpression();
            #region User 
            cfg.CreateMap<User, UserViewModel>().ReverseMap();
            cfg.CreateMap<UserViewModel, User>().ReverseMap();
            #endregion


            return cfg;
        }
    }
}
