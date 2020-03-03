using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetSport.Services.Authorizations
{
    public class ClaimRolesRequirement : IAuthorizationRequirement
    {
        public string Claim { get; }
        public IEnumerable<string> Roles { get; }

        public ClaimRolesRequirement(string claim, IEnumerable<string> roles)
        {
            Claim = claim ?? throw new ArgumentNullException(nameof(claim));
            Roles = roles ?? throw new ArgumentNullException(nameof(roles));
        }
    }
}
