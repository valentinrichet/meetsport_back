using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MeetSport.Dto.Events
{
    public class EventDto
    {
        [Required]
        public ulong Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public ulong Creator { get; set; }
        [Required]
        public PlaceDto Place { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public string Image { get; set; }
        [Required]
        public uint Likes { get; set; }
        [Required]
        public uint Dislikes { get; set; }
        [Required]
        public ICollection<ulong> EventAttendee { get; set; }
        [Required]
        public ICollection<EventCommentDto> EventComment { get; set; }
    }
}
