using MeetSport.Dbo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetSport.Repositories.Stub
{
    public class StubRepository<TEntity> : IRepository<TEntity>
    {
        public Task<TEntity> Add(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> Delete(ulong id)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> Get(ulong id)
        {
            throw new NotImplementedException();
        }

        public Task<List<TEntity>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> Update(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
