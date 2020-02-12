using System;
using System.Collections.Generic;

namespace MeetSport.Models
{
    public partial class User
    {
        public User()
        {
            Event = new HashSet<Event>();
            EventAttendee = new HashSet<EventAttendee>();
            EventChat = new HashSet<EventChat>();
            EventComment = new HashSet<EventComment>();
        }

        public ulong Id { get; set; }
        public string Mail { get; set; }
        public string HashedPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public ulong Role { get; set; }

        public virtual Role RoleNavigation { get; set; }
        public virtual AthleticUser AthleticUser { get; set; }
        public virtual MobileUser MobileUser { get; set; }
        public virtual ICollection<Event> Event { get; set; }
        public virtual ICollection<EventAttendee> EventAttendee { get; set; }
        public virtual ICollection<EventChat> EventChat { get; set; }
        public virtual ICollection<EventComment> EventComment { get; set; }
    }
}
