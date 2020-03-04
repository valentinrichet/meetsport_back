using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetSport.Business.Roles
{
    public interface IRoleBusiness<TEntity> : IBusiness<TEntity>
    {
        Task AddClaimToRole(ulong roleId, ulong claimId);
        Task RemoveClaimFromRole(ulong roleId, ulong claimId);
    }
}
