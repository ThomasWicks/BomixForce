
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
using System.Threading.Tasks;
using System;

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

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ModelContext>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdministratorRole",
                     policy => policy.RequireRole("Admin"));
                options.AddPolicy("RequireCompanyRole",
                     policy => policy.RequireRole("Company"));
                options.AddPolicy("RequirUserRole",
                    policy => policy.RequireRole("User"));
            });

            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddSingleton<IAuthorizationHandler, AuthHendler>();
            // Auto Mapper Configurations
            services.AddAutoMapper(typeof(Startup));

            //Injeção de dependencia
            services.AddScoped<ModelContext>();
            //services.AddScoped<IGenericRepository<User>, GenericRepository<User>>();
            //services.AddScoped<IGenericRepository<Access>, GenericRepository<Access>>();
            services.AddScoped<IGenericRepository<Company>, GenericRepository<Company>>();
            services.AddScoped<IGenericRepository<Order>, GenericRepository<Order>>();
            //services.AddScoped<IGenericRepository<Permission>, GenericRepository<Permission>>();
            services.AddScoped<IGenericRepository<Person>, GenericRepository<Person>>();
            //services.AddScoped<IGenericRepository<Bomix_Force.Data.Entities.Profile>, GenericRepository<Bomix_Force.Data.Entities.Profile>>();

            createRolesandUsers(createProvider(services)).Wait();
        }
        public IServiceProvider createProvider(IServiceCollection services)
        {
            return services.BuildServiceProvider();
        }

        private async Task createRolesandUsers(IServiceProvider serviceProvider)
        {
            var _roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var _userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            bool x = await _roleManager.RoleExistsAsync("Admin");
            if (!x)
            {
                // first we create Admin rool    
                var role = new IdentityRole();
                role.Name = "Admin";
                await _roleManager.CreateAsync(role);

                //Here we create a Admin super user who will maintain the website                   

                var user = new IdentityUser { UserName = "thomas.wicks@hotmail.com", Email = "thomas.wicks@hotmail.com" };


                string userPWD = "Admin123@";

                IdentityResult chkUser = await _userManager.CreateAsync(user, userPWD);

                //Add default User to Role Admin    
                if (chkUser.Succeeded)
                {
                    var result1 = await _userManager.AddToRoleAsync(user, "Admin");
                }
            }

            // creating Creating Manager role     
            x = await _roleManager.RoleExistsAsync("Manager");
            if (!x)
            {
                var role = new IdentityRole();
                role.Name = "Manager";
                await _roleManager.CreateAsync(role);
            }

            // creating Creating Employee role     
            x = await _roleManager.RoleExistsAsync("User");
            if (!x)
            {
                var role = new IdentityRole();
                role.Name = "User";
                await _roleManager.CreateAsync(role);
            }
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
