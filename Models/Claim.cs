using System;
using System.Collections.Generic;

namespace MeetSport.Models
{
    public partial class Claim
    {
        public Claim()
        {
            RoleClaim = new HashSet<RoleClaim>();
        }

        public ulong Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<RoleClaim> RoleClaim { get; set; }
    }
}
