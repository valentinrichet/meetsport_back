using MeetSport.Dbo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MeetSport.Repositories.Stub
{
    public class StubRepository<TEntity> : IRepository<TEntity>
    {
        public Task<TEntity> Add(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(object primaryKey)
        {
            throw new NotImplementedException();
        }

        public Task Delete(object primaryKeyA, object primaryKeyB)
        {
            throw new NotImplementedException();
        }

        public ValueTask<TEntity> Get(object primaryKey)
        {
            throw new NotImplementedException();
        }

        public ValueTask<TEntity> Get(object primaryKeyA, object primaryKeyB)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> Update(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
