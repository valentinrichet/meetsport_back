﻿using AutoMapper;
using MeetSport.Dbo;
using MeetSport.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MeetSport.Business
{
    public class Business<TEntity, TRepository> : IBusiness<TEntity>
    where TEntity : class
    where TRepository : IRepository<TEntity>
    {
        protected readonly TRepository _repository;
        protected readonly IMapper _mapper;
        protected readonly ILogger _logger;
        public Business(TRepository repository, IMapper mapper, ILogger<IBusiness<TEntity>> logger)
        {
            this._repository = repository;
            this._mapper = mapper;
            this._logger = logger;
        }

        public async Task<Dto> Add<Dto, CreationDto>(CreationDto creationDto)
        {
            TEntity entity = _mapper.Map<TEntity>(creationDto);
            entity = await _repository.Add(entity);
            Dto mappedEntity = _mapper.Map<Dto>(entity);
            return mappedEntity;
        }

        public Task Delete(object primaryKey)
        {
            return _repository.Delete(primaryKey);
        }

        public Task Delete(object primaryKeyA, object primaryKeyB)
        {
            return _repository.Delete(primaryKeyA, primaryKeyB);
        }

        public async Task<ICollection<Dto>> Get<Dto>(Expression<Func<TEntity, bool>> predicate)
        {
            IQueryable<TEntity> queryable = _repository.Get(predicate);
            ICollection<Dto> mappedEntities = await _mapper.ProjectTo<Dto>(queryable).ToListAsync();
            return mappedEntities;
        }

        public async Task<ICollection<Dto>> GetAll<Dto>()
        {
            IQueryable<TEntity> queryable = _repository.GetAll();
            ICollection<Dto> mappedEntities = await _mapper.ProjectTo<Dto>(queryable).ToListAsync();
            return mappedEntities;
        }

        public Task<Dto> GetFirstOrDefault<Dto>(Expression<Func<TEntity, bool>> predicate)
        {
            IQueryable<TEntity> queryable = _repository.Get(predicate);
            Dto mappedEntity = _mapper.ProjectTo<Dto>(queryable).FirstOrDefault();
            return Task.FromResult(mappedEntity);
        }

        public async Task<Dto> Update<Dto, UpdateDto>(UpdateDto updateDto, object primaryKey)
        {
            TEntity entity = await _repository.Get(primaryKey);
            _mapper.Map(updateDto, entity);
            entity = await _repository.Update(entity);
            Dto mappedEntity = _mapper.Map<Dto>(entity);
            return mappedEntity;
        }

        public async Task<Dto> Update<Dto, UpdateDto>(UpdateDto updateDto, object primaryKeyA, object primaryKeyB)
        {
            TEntity entity = await _repository.Get(primaryKeyA, primaryKeyB);
            _mapper.Map(updateDto, entity);
            entity = await _repository.Update(entity);
            Dto mappedEntity = _mapper.Map<Dto>(entity);
            return mappedEntity;
        }
    }
}
