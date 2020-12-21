using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Threading.Tasks;
using System.Security.Claims;

namespace Bomix_Force.Service
{
    [DisallowConcurrentExecution]
    public class ResetPassJob : IJob
    {
        private readonly ILogger<ResetPassJob> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ResetPassJob(ILogger<ResetPassJob> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }
        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation($"ResetPass: {DateTime.Now}");
            _logger.LogInformation($"JobKey: {context.JobDetail.Key}");
            //_logger.LogInformation($"User id: {_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value}");
            return Task.CompletedTask;
        }
    }
}
