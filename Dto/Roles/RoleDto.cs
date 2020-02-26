using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetSport.Dto.Roles
{
    public class RoleDto
    {
        public ulong Id { get; set; }
        public string Name { get; set; }

        public ICollection<ulong> RoleClaim { get; set; }
        public ICollection<ulong> User { get; set; }
    }
}
