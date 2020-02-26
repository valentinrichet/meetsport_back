using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetSport.Business
{
    public interface IBusiness<TEntity>
    {
        Task<Dto> Add<Dto, CreationDto>(CreationDto entityDto);
        Task<bool> Delete(params ulong[] primaryKey);
        Task<Dto> Get<Dto>(params ulong[] primaryKey);
        Task<ICollection<Dto>> GetAll<Dto>();
        Task<Dto> Update<Dto, UpdateDto>(UpdateDto entityDto, params ulong[] primaryKey);
    }
}
