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

            CreateMap<CreateUserDto, User>()
                .ForMember(
                    x => x.HashedPassword,
                    opt => opt.MapFrom(u => u.Password)
                );
        }
    }
}
