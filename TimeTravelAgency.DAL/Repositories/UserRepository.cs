using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTravelAgency.DAL.Interfaces;
using TimeTravelAgency.Domain.Entity;
using TimeTravelAgency.Domain.Enum;
using TimeTravelAgency.Domain.ViewModels.Account;

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

        public async Task<List<AccountViewModel>> SelectFullAccount(int id)
        {
            var account = await (from u in _db.Users
                                 where u.Id == id
                                 join p in _db.Uprofiles on u.Id equals p.Id
                                 select new AccountViewModel
                                 {
                                     Id = u.Id,
                                     ULogin = u.ULogin,
                                     HashPassword = u.HashPassword,
                                     URole = u.URole,
                                     FirstName = p.FirstName,
                                     LastName = p.LastName,
                                     Age = p.Age,
                                     Email = p.Email,
                                     Phone = p.Phone,
                                     Uaddress = p.Uaddress
                                 }).ToListAsync();
            return account;
        }
    }
}
