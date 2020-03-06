using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MeetSport.Dto.Users
{
    public class UserDto
    {
        /*
        public ulong Id { get; set; }
        public string Mail { get; set; }
        public string HashedPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public ulong Role { get; set; }

        public virtual Role RoleNavigation { get; set; }
        public virtual AthleticUser AthleticUser { get; set; }
        public virtual MobileUser MobileUser { get; set; }
        public virtual ICollection<Event> Event { get; set; }
        public virtual ICollection<EventAttendee> EventAttendee { get; set; }
        public virtual ICollection<EventChat> EventChat { get; set; }
        public virtual ICollection<EventComment> EventComment { get; set; }
         
    */


        [Required]
        public ulong Id { get; set; }
        [Required]
        [EmailAddress]
        public string Mail { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime Birthday { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        [Required]
        public ulong Role { get; set; }
        [Required]
        public ICollection<ulong> AttendedEvents { get; set; }
        [Required]
        public ICollection<ulong> CreatedEvents { get; set; }
    }
}
