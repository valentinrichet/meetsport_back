using System;
using System.Collections.Generic;

namespace MeetSport.Dbo
{
    public partial class AthleticUser
    {
        public ulong Id { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public decimal? Height { get; set; }
        public decimal? Weight { get; set; }

        public virtual User IdNavigation { get; set; }
    }
}
