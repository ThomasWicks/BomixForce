
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Bomix_Force.Data.Context;
using Bomix_Force.Repo;
using Bomix_Force.Data.Entities;
using Bomix_Force.Repo.Interface;
using AutoMapper;
using Bomix_Force.Util;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Bomix_Force
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddDbContext<ApplicationDbContext>(options =>
            //    options.UseSqlServer(
            //        Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<ModelContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("Mysqlconnection")));

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Delete", policy => policy.Requirements.Add(new AuthorizationHelper("Delete")));
                options.AddPolicy("Create", policy => policy.Requirements.Add(new AuthorizationHelper("Create")));
                options.AddPolicy("Edit", policy => policy.Requirements.Add(new AuthorizationHelper("Edit")));
                options.AddPolicy("Index", policy => policy.Requirements.Add(new AuthorizationHelper("Index")));
            }
            );

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ModelContext>();
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddSingleton<IAuthorizationHandler, AuthHendler>();
            // Auto Mapper Configurations
            AutoMapperConfig autoMapper = new AutoMapperConfig();
            services.AddSingleton(autoMapper);

            //Injeção de dependencia
            services.AddScoped<ModelContext>();
            services.AddScoped<IGenericRepository<User>, GenericRepository<User>>();
            //services.AddScoped<IGenericRepository<Access>, GenericRepository<Access>>();
            services.AddScoped<IGenericRepository<Company>, GenericRepository<Company>>();
            services.AddScoped<IGenericRepository<Order>, GenericRepository<Order>>();
            //services.AddScoped<IGenericRepository<Permission>, GenericRepository<Permission>>();
            services.AddScoped<IGenericRepository<Person>, GenericRepository<Person>>();
            //services.AddScoped<IGenericRepository<Bomix_Force.Data.Entities.Profile>, GenericRepository<Bomix_Force.Data.Entities.Profile>>();
            //services.AddScoped<IGenericRepository<UserLogin>, GenericRepository<UserLogin>>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Login}");
                endpoints.MapRazorPages();
            });
        }
    }
}
