using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MeetSport.Dto.Events
{
    public class UpdateEventDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public ulong? Creator { get; set; }
        public DateTime? Date { get; set; }
        public string Image { get; set; }
    }
}
