using System;
using System.Collections.Generic;

namespace MeetSport.Dbo
{
    public partial class Role : IEntity
    {
        public Role()
        {
            RoleClaim = new HashSet<RoleClaim>();
            User = new HashSet<User>();
        }

        public ulong Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<RoleClaim> RoleClaim { get; set; }
        public virtual ICollection<User> User { get; set; }
    }
}
