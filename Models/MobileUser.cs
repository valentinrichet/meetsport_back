using System;
using System.Collections.Generic;

namespace MeetSport.Models
{
    public partial class MobileUser
    {
        public ulong Id { get; set; }
        public string FirebaseToken { get; set; }

        public virtual User IdNavigation { get; set; }
    }
}
