using AutoMapper;
using MeetSport.Dbo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MeetSport.Repositories.Database
{
    public class DbRepository<TEntity, TContext> : IRepository<TEntity>
    where TEntity : class
    where TContext : DbContext

    {
        protected readonly TContext _context;
        public DbRepository(TContext context)
        {
            _context = context;
        }
        public async Task<TEntity> Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task Delete(TEntity entity)
        {
            if (entity != null)
            {
                try
                {
                    _context.Set<TEntity>().Remove(entity);
                    await _context.SaveChangesAsync();
                } 
                finally
                {
                }
            }
        }

        public async Task Delete(object primaryKey)
        {
            TEntity entity = await Get(primaryKey);

            if (entity != null)
            {
                _context.Set<TEntity>().Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task Delete(object primaryKeyA, object primaryKeyB)
        {
            TEntity entity = await Get(primaryKeyA, primaryKeyB);

            if (entity != null)
            {
                _context.Set<TEntity>().Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async ValueTask<TEntity> Get(object primaryKey)
        {
            TEntity entity = await _context.Set<TEntity>().FindAsync(primaryKey);
            return entity;
        }

        public async ValueTask<TEntity> Get(object primaryKeyA, object primaryKeyB)
        {
            TEntity entity = await _context.Set<TEntity>().FindAsync(primaryKeyA, primaryKeyB);
            return entity;
        }

        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
        {
            IQueryable<TEntity> queryable = _context.Set<TEntity>().Where(predicate);
            return queryable;
        }

        public IQueryable<TEntity> GetAll()
        {
            IQueryable<TEntity> queryable = _context.Set<TEntity>();
            return queryable;
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}