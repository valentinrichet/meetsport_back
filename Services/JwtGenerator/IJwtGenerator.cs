using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetSport.Services.JwtGenerator
{
    public interface IJwtGenerator
    {
        string GenerateToken(ulong userId, string role);
    }
}
