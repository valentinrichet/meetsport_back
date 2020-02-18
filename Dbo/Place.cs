using System;
using System.Collections.Generic;

namespace MeetSport.Dbo
{
    public partial class Place
    {
        public Place()
        {
            Event = new HashSet<Event>();
        }

        public ulong Id { get; set; }
        public string Title { get; set; }
        public string Adress { get; set; }
        public string City { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }

        public virtual ICollection<Event> Event { get; set; }
    }
}
