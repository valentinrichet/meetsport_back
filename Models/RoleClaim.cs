using System;
using System.Collections.Generic;

namespace MeetSport.Models
{
    public partial class RoleClaim
    {
        public ulong Role { get; set; }
        public ulong Claim { get; set; }

        public virtual Claim ClaimNavigation { get; set; }
        public virtual Role RoleNavigation { get; set; }
    }
}
