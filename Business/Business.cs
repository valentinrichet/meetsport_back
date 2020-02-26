using AutoMapper;
using MeetSport.Dbo;
using MeetSport.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetSport.Business.Database
{
    public class Business<TEntity, TRepository> : IBusiness<TEntity>
    where TEntity : class
    where TRepository : IRepository<TEntity>
    {
        protected readonly TRepository _repository;
        protected readonly IMapper _mapper;
        public Business(TRepository repository, IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }

        public async Task<Dto> Add<Dto, CreationDto>(CreationDto entityDto)
        {
            TEntity entity = _mapper.Map<TEntity>(entityDto);
            entity = await _repository.Add(entity);
            Dto mappedEntity = _mapper.Map<Dto>(entity);
            return mappedEntity;
        }

        public async Task<bool> Delete(params ulong[] primaryKey)
        {
            bool isDeleted = await _repository.Delete(primaryKey);
            return isDeleted;
        }

        public async Task<Dto> Get<Dto>(params ulong[] primaryKey)
        {
            TEntity entity = await _repository.Get(primaryKey);
            Dto mappedEntity = _mapper.Map<Dto>(entity);
            return mappedEntity;
        }

        public async Task<ICollection<Dto>> GetAll<Dto>()
        {
            IQueryable<TEntity> queryable = _repository.GetAll();
            ICollection<Dto> dtoList = await _mapper.ProjectTo<Dto>(queryable).ToListAsync();
            return dtoList;
        }

        public async Task<Dto> Update<Dto, UpdateDto>(UpdateDto entityDto, params ulong[] primaryKey)
        {
            TEntity entity = await this._repository.Get(primaryKey);
            _mapper.Map(entityDto, entity);
            await _repository.Update(entity);
            Dto mappedEntity = _mapper.Map<Dto>(entity);
            return mappedEntity;
        }
    }
}
