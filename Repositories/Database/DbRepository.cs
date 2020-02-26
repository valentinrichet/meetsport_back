using MeetSport.Dbo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetSport.Repositories.Database
{
    public class DbRepository<TEntity> : IRepository<TEntity>
    where TEntity : class
    {
        protected readonly MeetSportContext _context;
        public DbRepository(MeetSportContext context)
        {
            _context = context;
        }
        public async Task<TEntity> Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> Delete(params ulong[] primaryKey)
        {
            TEntity entity = await Get(primaryKey);

            if (entity == null)
            {
                return false;
            }

            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<TEntity> Get(params ulong[] primaryKey)
        {
            TEntity entity;

            if(primaryKey.Length == 1)
            {
                entity = await _context.Set<TEntity>().FindAsync(primaryKey[0]);
            }
            else
            {
                entity = await _context.Set<TEntity>().FindAsync(primaryKey);
            }
                          
            return entity;
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