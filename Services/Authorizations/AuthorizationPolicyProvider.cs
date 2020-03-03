using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetSport.Services.Authorizations
{
    public class AuthorizationPolicyProvider : DefaultAuthorizationPolicyProvider
    {
        private readonly AuthorizationOptions _options;
        private readonly IConfiguration _configuration;
        // TODO: DECLARE CLAIM REPOSITORY AND CREATE POLICIES FOR EVERY SINGLE CLAIM (USE BOOLEAN TO CHECK IF EVER CREATED)
        public AuthorizationPolicyProvider(IOptions<AuthorizationOptions> options, IConfiguration configuration) : base(options)
        {
            _options = options.Value;
            _configuration = configuration;
        }

        public override async Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
        {
            // Check static policies first
            AuthorizationPolicy policy = await base.GetPolicyAsync(policyName);

            if (policy == null)
            {
                policy = new AuthorizationPolicyBuilder()
                    .AddRequirements(new ClaimRequirement(policyName))
                    .Build();

                // Add policy to the AuthorizationOptions, so we don't have to re-create it each time
                _options.AddPolicy(policyName, policy);
            }

            return policy;
        }
    }
}
