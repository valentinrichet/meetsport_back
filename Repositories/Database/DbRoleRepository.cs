using MeetSport.Dbo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetSport.Repositories.Database
{
    public class DbRoleRepository : DbRepository<Role, MeetSportContext>
    {
        public DbRoleRepository(MeetSportContext context) : base(context)
        {

        }
        // We can add new methods specific to the movie repository here in the future
    }
}
