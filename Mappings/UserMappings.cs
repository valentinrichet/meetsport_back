using AutoMapper;
using MeetSport.Dbo;
using MeetSport.Dto.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetSport.Mappings
{
    public class UserMappings : Profile
    {
        public UserMappings()
        {
            CreateMap<User, UserDto>()
                .ForMember(
                    x => x.AttendedEvents,
                    opt => opt.MapFrom(u => u.EventAttendee.Select(ea => ea.Event).ToList())
                )
                .ForMember(
                    x => x.CreatedEvents,
                    opt => opt.MapFrom(u => u.Event.Select(e => e.Id).ToList())
                );

            CreateMap<CreateUserDto, User>();

            CreateMap<UpdateUserDto, User>()
                .ForMember(
                    x => x.Role,
                    opt => opt.Condition((src) => src.Role != null)
                )
                .ForMember(
                    x => x.Birthday,
                    opt => opt.Condition((src) => src.Birthday != null)
                )
                .ForAllOtherMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
