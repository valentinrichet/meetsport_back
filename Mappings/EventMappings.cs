using AutoMapper;
using MeetSport.Dbo;
using MeetSport.Dto.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetSport.Mappings
{
    public class EventMappings : Profile
    {
        public EventMappings()
        {
            CreateMap<EventComment, EventCommentDto>();

            CreateMap<CreateEventDto, Event>();

            CreateMap<Event, EventDto>()
                .ForMember(
                    x => x.EventAttendee,
                    opt => opt.MapFrom(r => r.EventAttendee.Select(ea => ea.User).ToList())
                )
                .ForMember(
                    x => x.EventComment,
                    opt => opt.MapFrom(s => s.EventComment)
                );

            CreateMap<UpdateEventDto, Event>()
               .ForMember(
                   x => x.Date,
                   opt => opt.Condition((src) => src.Date != null)
               )
               .ForAllOtherMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
