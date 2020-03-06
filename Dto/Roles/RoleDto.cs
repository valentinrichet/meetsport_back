using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MeetSport.Dto.Roles
{
    public class RoleDto
    {
        [Required]
        public ulong Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public ICollection<ulong> Claims { get; set; }
    }
}
