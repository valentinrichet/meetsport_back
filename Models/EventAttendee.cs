using System;
using System.Collections.Generic;

namespace MeetSport.Models
{
    public partial class EventAttendee
    {
        public ulong Event { get; set; }
        public ulong User { get; set; }

        public virtual Event EventNavigation { get; set; }
        public virtual User UserNavigation { get; set; }
    }
}
