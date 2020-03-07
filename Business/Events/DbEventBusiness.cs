using AutoMapper;
using MeetSport.Dbo;
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
    }
}
