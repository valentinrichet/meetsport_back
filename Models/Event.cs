using System;
using System.Collections.Generic;

namespace MeetSport.Models
{
    public partial class Event
    {
        public Event()
        {
            EventAttendee = new HashSet<EventAttendee>();
            EventChat = new HashSet<EventChat>();
            EventComment = new HashSet<EventComment>();
        }

        public ulong Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ulong Creator { get; set; }
        public ulong Place { get; set; }
        public DateTime Date { get; set; }
        public uint Likes { get; set; }
        public uint Dislikes { get; set; }

        public virtual User CreatorNavigation { get; set; }
        public virtual Place PlaceNavigation { get; set; }
        public virtual ICollection<EventAttendee> EventAttendee { get; set; }
        public virtual ICollection<EventChat> EventChat { get; set; }
        public virtual ICollection<EventComment> EventComment { get; set; }
    }
}
