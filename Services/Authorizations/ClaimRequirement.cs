using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetSport.Services.Authorizations
{
    public class ClaimRequirement : IAuthorizationRequirement
    {
        public string Claim { get; }

        public ClaimRequirement(string claim)
        {
            Claim = claim ?? throw new ArgumentNullException(nameof(claim));
        }
    }
}
