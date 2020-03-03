using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetSport.Repositories
{
    public interface IUserRepository<TEntity> : IRepository<TEntity>
    {
        Task<TEntity> FindByMail(string mail);
    }
}
