using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetSport.Services.JwtGenerator
{
    public interface IJwtGenerator
    {
        string GenerateToken(ulong userId, ulong roleId);
        string GenerateToken(ulong userId, ulong roleId, IEnumerable<string> userClaims);
    }
}
