using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MeetSport.Dto.Users
{
    public class UpdateUserDto
    {
        [EmailAddress]
        public string Mail { get; set; }
        [MinLength(6)]
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? Birthday { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public ulong? Role { get; set; }
    }
}
