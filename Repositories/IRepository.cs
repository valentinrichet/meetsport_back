using MeetSport.Dbo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetSport.Repositories
{
    public interface IRepository<T>
    {
        Task<List<T>> GetAll();
        Task<T> Get(ulong id);
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        Task<T> Delete(ulong id);
    }
}
