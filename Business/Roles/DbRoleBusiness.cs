using AutoMapper;
using MeetSport.Dbo;
using MeetSport.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetSport.Business.Roles
{
    public class DbRoleBusiness : Business<Role, IRepository<Role>>, IRoleBusiness<Role>
    {
        protected readonly IRepository<RoleClaim> _roleClaimRepository;

        public DbRoleBusiness(IRepository<Role> repository, IRepository<RoleClaim> roleClaimRepository, IMapper mapper) : base(repository, mapper)
        {
            _roleClaimRepository = roleClaimRepository;
        }

        public async Task AddClaimToRole(ulong roleId, ulong claimId)
        {
            RoleClaim roleClaim = new RoleClaim { Role = roleId, Claim = claimId };
            await _roleClaimRepository.Add(roleClaim);
        }

        public async Task RemoveClaimFromRole(ulong roleId, ulong claimId)
        {
            await _roleClaimRepository.Delete(roleId, claimId);
        }
    }
}
