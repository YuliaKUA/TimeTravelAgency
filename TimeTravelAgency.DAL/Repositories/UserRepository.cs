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
    public class UserRepository : IBaseRepository<User>
    {
        private readonly TimeTravelAgencyContext _db;

        public UserRepository(TimeTravelAgencyContext db)
        {
            _db = db;
        }
        public async Task Create(User entity)
        {
            _db.Users.Add(entity);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(User entity)
        {
            _db.Users.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public IQueryable<User> GetAll()
        {
            return _db.Users;
        }

        public async Task<User> GetById(int id)
        {
            return await _db.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User> GetByName(string name)
        {
            return await _db.Users.FirstOrDefaultAsync(x => x.ULogin == name);
        }

        public Task<List<User>> SelectAll()
        {
            return _db.Users.ToListAsync();
        }

        public async Task<User> Update(User entity)
        {
            _db.Users.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }
    }
}
