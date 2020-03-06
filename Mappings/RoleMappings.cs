using AutoMapper;
using MeetSport.Dbo;
using MeetSport.Dto.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetSport.Mappings
{
    public class RoleMappings : Profile
    {
        public RoleMappings()
        {
            CreateMap<Role, RoleDto>()
                .ForMember(
                    x => x.Claims,
                    opt => opt.MapFrom(r => r.RoleClaim.Select(rc => rc.Claim).ToList())
                );

            CreateMap<CreateRoleDto, Role>();

            CreateMap<UpdateRoleDto, Role>();
        }
    }
}
