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
    public class ProfileRepository : IBaseRepository<Uprofile>
    {
        private readonly TimeTravelAgencyContext _db;

        public ProfileRepository(TimeTravelAgencyContext db)
        {
            _db = db;
        }
        public async Task Create(Uprofile entity)
        {
            _db.Uprofiles.Add(entity);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(Uprofile entity)
        {
            _db.Uprofiles.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public IQueryable<Uprofile> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<Uprofile> GetById(int id)
        {
            return await _db.Uprofiles.FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<Uprofile> GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public Task<List<Uprofile>> SelectAll()
        {
            return _db.Uprofiles.ToListAsync();
        }

        public Task<IQueryable<ExtendedOrder>> SelectExtendedOrder(int userId, StatusOrder status)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Order>> SelectOrder(int userId, StatusOrder status)
        {
            throw new NotImplementedException();
        }

        public async Task<Uprofile> Update(Uprofile entity)
        {
            _db.Uprofiles.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }

        public Task UpdateRange(IEnumerable<Uprofile> values)
        {
            throw new NotImplementedException();
        }
    }
}
