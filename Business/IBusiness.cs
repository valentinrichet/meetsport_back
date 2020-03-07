using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MeetSport.Business
{
    public interface IBusiness<TEntity>
    {
        Task<Dto> Add<Dto, CreationDto>(CreationDto entityDto);
        Task Delete(object primaryKey);
        Task Delete(object primaryKeyA, object primaryKeyB);
        Task<ICollection<Dto>> Get<Dto>(Expression<Func<TEntity, bool>> where);
        Task<ICollection<Dto>> GetAll<Dto>();
        Task<Dto> GetFirstOrDefault<Dto>(Expression<Func<TEntity, bool>> where);
        Task<Dto> Update<Dto, UpdateDto>(UpdateDto entityDto, object primaryKey);
        Task<Dto> Update<Dto, UpdateDto>(UpdateDto entityDto, object primaryKeyA, object primaryKeyB);
    }
}
