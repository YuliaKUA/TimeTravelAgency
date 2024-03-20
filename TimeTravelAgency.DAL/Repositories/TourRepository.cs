using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTravelAgency.DAL.Interfaces;
using TimeTravelAgency.Domain.Entity;

namespace TimeTravelAgency.DAL.Repositories
{
    public class TourRepository : IBaseRepository<Tour>
    {
        private readonly TimeTravelAgencyContext _db;

        public TourRepository(TimeTravelAgencyContext db)
        {
            _db = db;
        }

        public async Task Create(Tour entity)
        {
            _db.Tours.Add(entity);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(Tour entity)
        {
            _db.Tours.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public IQueryable<Tour> GetAll()
        {
            return _db.Tours;
        }

        public async Task<Tour> GetById(int id)
        {
            return await _db.Tours.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Tour> GetByName(string name)
        {
            return await _db.Tours.FirstOrDefaultAsync(x => x.Title == name);
        }

        public Task<List<Tour>> SelectAll()
        {
            return _db.Tours.ToListAsync();
        }

        public Task<IQueryable<ExtendedOrder>> SelectExtendedOrder(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<Tour> Update(Tour entity)
        {
            _db.Tours.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }
    }
}
