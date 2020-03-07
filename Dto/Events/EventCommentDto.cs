using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetSport.Dto.Events
{
    public class EventCommentDto
    {
        public ulong Id { get; set; }
        public ulong User { get; set; }
        public DateTime Date { get; set; }
        public string Comment { get; set; }
        public uint Likes { get; set; }
    }
}
