//using Bomix_Force.Data.Context;
//using Bomix_Force.Data.Entities;
//using Bomix_Force.Repo;
//using Bomix_Force.Repo.Interface;
//using Microsoft.Extensions.DependencyInjection;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Bomix_Force
//{
//    public static class DependencyInjectionConfig
//    {
//        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
//        {
//            services.AddScoped<ModelContext>();
//            services.AddScoped<IGenericRepository<User>, GenericRepository<User>>();
//            services.AddScoped<IGenericRepository<Access>, GenericRepository<Access>>();
//            services.AddScoped<IGenericRepository<Company>, GenericRepository<Company>>();
//            services.AddScoped<IGenericRepository<Order>, GenericRepository<Order>>();
//            services.AddScoped<IGenericRepository<Permission>, GenericRepository<Permission>>();
//            services.AddScoped<IGenericRepository<Person>, GenericRepository<Person>>();
//            services.AddScoped<IGenericRepository<Profile>, GenericRepository<Profile>>();
//            services.AddScoped<IGenericRepository<UserLogin>, GenericRepository<UserLogin>>();
//            services.AddTransient<IPersonService, PersonService>();

//            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
//            services.AddScoped<IUser, AspNetUser>();

//            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

//            return services;
//        }
//    }
//}
