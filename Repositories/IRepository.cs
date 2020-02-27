using MeetSport.Dbo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetSport.Repositories
{
    public interface IRepository<TEntity>
    {
        Task<TEntity> Add(TEntity entity);
        Task Delete(params ulong[] primaryKey);
        Task<TEntity> Get(params ulong[] primaryKey);
        IQueryable<TEntity> GetAll();
        Task<TEntity> Update(TEntity entity);
    }
}
