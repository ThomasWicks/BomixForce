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
using Bomix_Force.AppServices.Interface;
using Bomix_Force.AppServices;
using Microsoft.AspNetCore.Mvc.Infrastructure;

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
            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));
            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddSingleton<IEmailSender, EmailSender>();
            services.AddSingleton<IEmailSender, EmailSender>();
            services.AddSingleton<IAuthorizationHandler, AuthHendler>();
            // Auto Mapper Configurations
            services.AddAutoMapper(typeof(Startup));

            //Injeção de dependencia
            services.AddScoped<ModelContext>();
            services.AddScoped<IGenericRepository<Company>, GenericRepository<Company>>();
            //services.AddScoped<IGenericRepository<Order>, GenericRepository<Order>>();
            services.AddScoped<IGenericRepository<Person>, GenericRepository<Person>>();
            services.AddScoped<IGenericRepository<Employee>, GenericRepository<Employee>>();
            services.AddScoped<INonconformityRepository, NonconformityRepository>();
            services.AddScoped<IGenericRepository<Bomix_PedidoVenda>, GenericRepository<Bomix_PedidoVenda>>();
            services.AddScoped<IGenericRepository<Bomix_PedidoVendaItem>, GenericRepository<Bomix_PedidoVendaItem>>();
            services.AddScoped<IPedidoVendaRepository, PedidoVendaRepository>();
            services.AddScoped<IPedidoItemRepository, PedidoItemRepository>();

            //services.AddScoped<IGenericRepository<Item>, GenericRepository<Item>>();
            //services.AddScoped<IGenericRepository<Document>, GenericRepository<Document>>();
            services.AddScoped<IGenericRepository<IdentityUser>, GenericRepository<IdentityUser>>();
        }

        private async Task CreateRolesandUsers(IServiceProvider serviceProvider)
        {
            var _roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var _userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            bool existAdmin = await _roleManager.RoleExistsAsync("Admin");
            bool existEmployee = await _roleManager.RoleExistsAsync("Employee");
            bool existCompany = await _roleManager.RoleExistsAsync("Company");
            bool existUser = await _roleManager.RoleExistsAsync("User");

            if (!existAdmin)
            {
                var admin = new IdentityRole
                {
                    Name = "Admin"
                };
                await _roleManager.CreateAsync(admin);
            }
            else if (!existEmployee)
            {
                var Employee = new IdentityRole
                {
                    Name = "Employee"
                };
                await _roleManager.CreateAsync(Employee);
            }
            else if(!existCompany)
            {
                var companyRole = new IdentityRole
                {
                    Name = "Company"
                };
                await _roleManager.CreateAsync(companyRole);
            }
            else if (!existUser)
            {
                var user = new IdentityRole
                {
                    Name = "User"
                };
                await _roleManager.CreateAsync(user);
            }

            var _dbSetCompany = serviceProvider.GetRequiredService<IGenericRepository<Company>>();
            var _dbSetEmployee = serviceProvider.GetRequiredService<IGenericRepository<Employee>>();
            var Company = _dbSetCompany.Get(c => c.IdentityUserId == null);
            var EmployeeList = _dbSetEmployee.Get(c => c.IdentityUserId == null);
            foreach (var item in EmployeeList)
            {
                var user = new IdentityUser { UserName = item.Login, EmailConfirmed = false };

                string userPWD = "Admin123@";

                var chkUser = await _userManager.CreateAsync(user, userPWD);

                //Add default User to Role Admin
                if (chkUser.Succeeded)
                {
                    _ = await _userManager.AddToRoleAsync(user, "Employee");
                    item.IdentityUser = user;
                    _dbSetEmployee.Update(item);
                }
            }
            foreach (var item in Company)
            {
                var user = new IdentityUser { UserName = item.Cnpj, EmailConfirmed = false };

                string userPWD = "Admin123@";

                var chkUser = await _userManager.CreateAsync(user, userPWD);

                //Add default User to Role Admin
                if (chkUser.Succeeded)
                {
                    _ = await _userManager.AddToRoleAsync(user, "Company");
                    item.IdentityUser = user;
                    _dbSetCompany.Update(item);
                }

            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider services)
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
            app.UseStaticFiles();
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
            CreateRolesandUsers(services).Wait();
        }

    }
}