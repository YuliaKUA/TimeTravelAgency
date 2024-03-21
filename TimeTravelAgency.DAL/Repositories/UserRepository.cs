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
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly TimeTravelAgencyContext _db;

        public UserRepository(TimeTravelAgencyContext context) : base(context)
        {
            _db = context;
        }
        public async Task<User> GetById(int id)
        {
            return await _db.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User> GetByName(string name)
        {
            return await _db.Users.FirstOrDefaultAsync(x => x.ULogin == name);
        }

    }
}
