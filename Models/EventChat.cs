using System;
using System.Collections.Generic;

namespace MeetSport.Models
{
    public partial class EventChat
    {
        public ulong Id { get; set; }
        public ulong Event { get; set; }
        public ulong User { get; set; }
        public DateTime Date { get; set; }
        public string Message { get; set; }

        public virtual Event EventNavigation { get; set; }
        public virtual User UserNavigation { get; set; }
    }
}
