using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetSport.Services.Authorizations
{
    public class ClaimRequirementHandler : AuthorizationHandler<ClaimRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ClaimRequirement requirement)
        {
            if (context.User.Claims.Any(c => c.Type == JwtGenerator.JwtGenerator.CustomClaimTypes.Claims && c.Value == requirement.Claim))
            {
                context.Succeed(requirement);
            }
                
            return Task.CompletedTask;
        }
    }
}
