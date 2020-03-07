using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MeetSport.Dto.Events
{
    public class CreateEventDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public ulong Creator { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public string Image { get; set; }
    }
}
