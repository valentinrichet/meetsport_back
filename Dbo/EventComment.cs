using System;
using System.Collections.Generic;

namespace MeetSport.Dbo
{
    public partial class EventComment
    {
        public ulong Id { get; set; }
        public ulong Event { get; set; }
        public ulong User { get; set; }
        public DateTime Date { get; set; }
        public string Comment { get; set; }
        public uint Likes { get; set; }

        public virtual Event EventNavigation { get; set; }
        public virtual User UserNavigation { get; set; }
    }
}
