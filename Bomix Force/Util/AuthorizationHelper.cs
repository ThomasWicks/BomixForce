using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bomix_Force.Util
{
    public class AuthorizationHelper : IAuthorizationRequirement
    {
        public string Permission { get; set; }

        public AuthorizationHelper(string permission)
        {
            Permission = permission;
        }
    }

    public class AuthHendler : AuthorizationHandler<AuthorizationHelper>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AuthorizationHelper requirement)
        {
            if (context.User.HasClaim(c => c.Type == "Permission" && c.Value.Contains(requirement.Permission)))
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
