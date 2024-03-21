using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTravelAgency.DAL.Interfaces;
using TimeTravelAgency.Domain.Entity;
using TimeTravelAgency.Domain.Enum;

namespace TimeTravelAgency.DAL.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly TimeTravelAgencyContext _context;
        private readonly DbSet<T> _dbSet;

        public BaseRepository(TimeTravelAgencyContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }
        public async Task Create(T entity)
        {
            _dbSet.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet;
        }

        public Task<List<T>> SelectAll()
        {
            return _dbSet.ToListAsync();
        }

        public async Task<T> Update(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task UpdateRange(IEnumerable<T> values)
        {
            _dbSet.UpdateRange(values);
            await _context.SaveChangesAsync();
        }
    }
}
