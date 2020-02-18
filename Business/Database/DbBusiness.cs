using MeetSport.Dbo;
using MeetSport.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetSport.Business.Database
{
    public class DbBusiness<TEntity, TRepository> : IBusiness<TEntity>
    where TEntity : class, IEntity
    where TRepository : IRepository<TEntity>
    {
        private readonly TRepository _repository;
        public DbBusiness(TRepository repository)
        {
            this._repository = repository;
        }
        public async Task<TEntity> Add(TEntity entity)
        {
            return await _repository.Add(entity);
        }

        public async Task<TEntity> Delete(ulong id)
        {
            return await _repository.Delete(id);
        }

        public async Task<TEntity> Get(ulong id)
        {
            return await _repository.Get(id);
        }

        public async Task<List<TEntity>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            return await _repository.Update(entity);
        }
    }
}
