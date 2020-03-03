using MeetSport.Dbo;
using MeetSport.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace MeetSport.Services.Authorizations
{
    public class AuthorizationPolicyProvider : DefaultAuthorizationPolicyProvider
    {
        private static SemaphoreSlim semaphore = new SemaphoreSlim(1);
        private readonly AuthorizationOptions _options;
        private readonly IConfiguration _configuration;
        //private readonly IRepository<RoleClaim> _repository;
        private bool _hasLoadedPolicies;

        public AuthorizationPolicyProvider(IOptions<AuthorizationOptions> options, IConfiguration configuration) : base(options)
        {
            _options = options.Value;
            _configuration = configuration;
            //_repository = repository;
            _hasLoadedPolicies = false;
        }

        private Task LoadPolicies()
        {
            // DOES NOT WORK BECAUSE SCOPED SERVICE USED IN SINGLETON AND DBCONTEXT IS A SCOPED SERVICE
            /*
            var roleClaimList = await _repository.GetAll().Select(roleClaim => new { RoleName = roleClaim.RoleNavigation.Name, ClaimName = roleClaim.ClaimNavigation.Name }).GroupBy(roleClaim => roleClaim.ClaimName).ToListAsync();
            roleClaimList.ForEach(roleClaim =>
            {
                string claim = roleClaim.Key;
                IEnumerable<string> roles = roleClaim.Select(roleClaim => roleClaim.RoleName).ToList();
                AuthorizationPolicy policy = new AuthorizationPolicyBuilder()
                    .AddRequirements(new ClaimRolesRequirement(claim, roles))
                    .Build();
                _options.AddPolicy(claim, policy);
            });
            */
            IDictionary<string, IEnumerable<string>> claimRolesDictionary = new Dictionary<string, IEnumerable<string>>()
            {
                [ClaimNames.RoleRead] = new List<string> { RoleNames.Admin },
                [ClaimNames.RoleWrite] = new List<string> { RoleNames.Admin },
                [ClaimNames.ClaimRead] = new List<string> { RoleNames.Admin },
                [ClaimNames.ClaimWrite] = new List<string> { RoleNames.Admin },
                [ClaimNames.RoleClaimRead] = new List<string> { RoleNames.Admin },
                [ClaimNames.RoleClaimWrite] = new List<string> { RoleNames.Admin },
                [ClaimNames.UserRead] = new List<string> { RoleNames.User, RoleNames.Admin },
                [ClaimNames.UserWrite] = new List<string> { RoleNames.User, RoleNames.Admin },
                [ClaimNames.PlaceRead] = new List<string> { RoleNames.User, RoleNames.Admin },
                [ClaimNames.PlaceWrite] = new List<string> { RoleNames.User, RoleNames.Admin },
                [ClaimNames.EventRead] = new List<string> { RoleNames.User, RoleNames.Admin },
                [ClaimNames.EventWrite] = new List<string> { RoleNames.User, RoleNames.Admin },
                [ClaimNames.EventAttendeeRead] = new List<string> { RoleNames.User, RoleNames.Admin },
                [ClaimNames.EventAttendeeWrite] = new List<string> { RoleNames.User, RoleNames.Admin },
                [ClaimNames.EventCommentRead] = new List<string> { RoleNames.User, RoleNames.Admin },
                [ClaimNames.EventCommentWrite] = new List<string> { RoleNames.User, RoleNames.Admin },
                [ClaimNames.EventChatRead] = new List<string> { RoleNames.User, RoleNames.Admin },
                [ClaimNames.EventChatWrite] = new List<string> { RoleNames.User, RoleNames.Admin }
            };

            foreach (KeyValuePair<string, IEnumerable<string>> claimRoles in claimRolesDictionary)
            {
                AuthorizationPolicy policy = new AuthorizationPolicyBuilder()
                    .AddRequirements(new ClaimRolesRequirement(claimRoles.Key, claimRoles.Value))
                    .Build();
                _options.AddPolicy(claimRoles.Key, policy);
            }

            return Task.CompletedTask;
        }

        public override async Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
        {
            if (!_hasLoadedPolicies)
            {
                await semaphore.WaitAsync();
                try
                {
                    if (!_hasLoadedPolicies)
                    {
                        await LoadPolicies();
                        _hasLoadedPolicies = true;
                    }
                }
                finally
                {
                    semaphore.Release();
                }
            }

            AuthorizationPolicy policy = await base.GetPolicyAsync(policyName);

            return policy;
        }
    }
}
