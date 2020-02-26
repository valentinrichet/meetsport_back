using MeetSport.Dbo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetSport.Repositories.Database
{
    public class DbRoleRepository : DbRepository<Role>
    {
        public DbRoleRepository(MeetSportContext context) : base(context)
        {
        }
    }
}
