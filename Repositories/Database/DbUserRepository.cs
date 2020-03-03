using MeetSport.Dbo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetSport.Repositories.Database
{
    public class DbUserRepository<TContext> : DbRepository<User, TContext>, IUserRepository<User>
    where TContext : DbContext
    {
        public DbUserRepository(TContext context) : base(context)
        {
        }

        public async Task<User> FindByMail(string mail)
        {
            User user = await GetAll().Where(user => user.Mail == mail).FirstAsync();
            return user;
        }
    }
}