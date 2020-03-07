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
            CreateMap<Place, PlaceDto>();

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
                )
                .ForMember(
                    x => x.Place,
                    opt => opt.MapFrom(s => s.PlaceNavigation)
                );

            CreateMap<UpdateEventDto, Event>()
                .ForMember(
                   x => x.Creator,
                   opt => opt.Condition((src) => src.Creator != null)
               )
               .ForMember(
                   x => x.Date,
                   opt => opt.Condition((src) => src.Date != null)
               )
               .ForAllOtherMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
