using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetSport.Services.Authorizations
{
    public class ClaimRolesRequirementHandler : AuthorizationHandler<ClaimRolesRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ClaimRolesRequirement requirement)
        {
            if (context.User.Claims.Where(c => c.Type == JwtGenerator.JwtGenerator.CustomClaimTypes.Role).Any(c => requirement.Roles.Any(r => r == c.Value)))
            {
                context.Succeed(requirement);
            }
                
            return Task.CompletedTask;
        }
    }
}
