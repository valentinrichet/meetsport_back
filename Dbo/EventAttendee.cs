using System;
using System.Collections.Generic;

namespace MeetSport.Dbo
{
    public partial class EventAttendee
    {
        public ulong Event { get; set; }
        public ulong User { get; set; }
        public string State { get; set; }

        public virtual Event EventNavigation { get; set; }
        public virtual User UserNavigation { get; set; }
    }
}
