using AutoMapper;
using MeetSport.Dbo;
using MeetSport.Dto.Events;
using MeetSport.Exceptions;
using MeetSport.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetSport.Business.Events
{
    public class DbEventBusiness : Business<Event, IRepository<Event>>, IEventBusiness<Event>
    {
        protected readonly IRepository<EventAttendee> _eventAttendeeRepository;

        public DbEventBusiness(IRepository<Event> repository, IRepository<EventAttendee> eventAttendeeRepository, IMapper mapper, ILogger<IEventBusiness<Event>> logger) : base(repository, mapper, logger)
        {
            _eventAttendeeRepository = eventAttendeeRepository;
        }

        public async Task<Dto> CreateEvent<Dto>(CreateEventDto createEventDto)
        {
            Event eventEntity = _mapper.Map<Event>(createEventDto);
            eventEntity.Place = Convert.ToUInt64(new Random().Next(1, 4));
            eventEntity = await _repository.Add(eventEntity);
            Dto mappedEvent = _mapper.Map<Dto>(eventEntity);
            return mappedEvent;
        }
    }
}
