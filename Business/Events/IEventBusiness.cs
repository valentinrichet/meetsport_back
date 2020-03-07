using MeetSport.Dto.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetSport.Business.Events
{
    public interface IEventBusiness<TEntity> : IBusiness<TEntity>
    {
        Task<Dto> CreateEvent<Dto>(CreateEventDto createEventDto);
    }
}
