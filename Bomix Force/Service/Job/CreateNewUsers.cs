using Bomix_Force.Data.Entities;
using Bomix_Force.Repo.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bomix_Force.Service.Job
{
    [DisallowConcurrentExecution]
    public class CreateNewUsers : IJob
    {

        private readonly ILogger<CreateNewUsers> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IServiceProvider _serviceProvider;
        public CreateNewUsers(ILogger<CreateNewUsers> logger, IHttpContextAccessor httpContextAccessor, IServiceProvider serviceProvider)
        {
            _httpContextAccessor = httpContextAccessor;
            _serviceProvider = serviceProvider;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _userManager = scope.ServiceProvider.GetService<UserManager<IdentityUser>>();
                var _roleManager = scope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
                var _dbSetCompany = scope.ServiceProvider.GetService<IGenericRepository<Company>>();
                var _dbSetEmployee = scope.ServiceProvider.GetService<IGenericRepository<Employee>>();

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
                else if (!existCompany)
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

                var Company = _dbSetCompany.Get(c => c.IdentityUserId == null);
                var EmployeeList = _dbSetEmployee.Get(c => c.IdentityUserId == null);
                foreach (var item in EmployeeList)
                {
                    var user = new IdentityUser { UserName = item.Login, EmailConfirmed = false };

                    string userPWD = item.Login + "@" + DateTime.Now.Year.ToString();

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

                    string userPWD = item.Cnpj.Substring(0,8);

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
        }
    }
}
