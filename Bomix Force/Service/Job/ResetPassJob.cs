using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Threading.Tasks;
using System.Security.Claims;
using Bomix_Force.Repo.Interface;
using Bomix_Force.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Bomix_Force.Service
{
    [DisallowConcurrentExecution]
    public class ResetPassJob : IJob
    {
        private readonly ILogger<ResetPassJob> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IServiceProvider _serviceProvider;
        public ResetPassJob(ILogger<ResetPassJob> logger, IHttpContextAccessor httpContextAccessor, IServiceProvider serviceProvider)
        {
            _httpContextAccessor = httpContextAccessor;
            _serviceProvider = serviceProvider;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _userManager = scope.ServiceProvider.GetService<UserManager<IdentityUser>>();
                var Employee = _userManager.GetUsersInRoleAsync("Employee").GetAwaiter().GetResult();
                var Company = _userManager.GetUsersInRoleAsync("Company").GetAwaiter().GetResult();
                var User = _userManager.GetUsersInRoleAsync("User").GetAwaiter().GetResult();
                var Admin = _userManager.GetUsersInRoleAsync("Admin").GetAwaiter().GetResult();

                foreach (var userAdmin in Admin)
                {
                    userAdmin.EmailConfirmed = false;
                    await _userManager.UpdateAsync(userAdmin);
                }
                foreach (var userEmployee in Employee)
                {
                    userEmployee.EmailConfirmed = false;
                    await _userManager.UpdateAsync(userEmployee);
                }
                foreach (var userCompany in Company)
                {
                    userCompany.EmailConfirmed = false;
                    await _userManager.UpdateAsync(userCompany);
                }
                foreach (var userUser in User)
                {
                    userUser.EmailConfirmed = false;
                    await _userManager.UpdateAsync(userUser);
                }
            }
        }
    }
}
