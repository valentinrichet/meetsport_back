using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MeetSport.Dto.Users
{
    public class UserDto
    {
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
