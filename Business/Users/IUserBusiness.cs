using MeetSport.Dbo;
using MeetSport.Dto.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetSport.Business.Users
{
    public interface IUserBusiness<TEntity> : IBusiness<TEntity>
    {
        Task<string> Authenticate(UserAuthenticationDto userAuthenticationDto);
    }
}
