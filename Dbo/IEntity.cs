using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetSport.Dbo
{
    public interface IEntity
    {
        ulong Id { get; set; }
    }
}
