using MeetSport.Services.Authorizations;
using MeetSport.Services.JwtGenerator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetSport.Controllers
{
    [Authorize]
    public abstract class AuthorizedController : Controller
    {
        protected string UserId 
        {
            get
            {
                return User.FindFirst(claim => claim.Type == JwtGenerator.CustomClaimTypes.Id).Value;
            }
        }

        protected string Role
        {
            get
            {
                return User.FindFirst(claim => claim.Type == JwtGenerator.CustomClaimTypes.Role).Value;
            }
        }


        public AuthorizedController(ILogger<AuthorizedController> logger) : base(logger)
        {
        }

        protected bool IsAdmin()
        {
            return User.FindFirst(claim => claim.Type == JwtGenerator.CustomClaimTypes.Role && claim.Value == RoleNames.Admin) != null;
        }

        protected bool IsUser(ulong id)
        {
            return User.FindFirst(claim => claim.Type == JwtGenerator.CustomClaimTypes.Id && claim.Value == id.ToString()) != null;
        }
    }
}
