using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetSport.Options
{
    public class HashingOptions
    {
        public string Salt { get; set; }
        public string Key { get; set; }
        public int Iterations { get; set; }
    }
}
