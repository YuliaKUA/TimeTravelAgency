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
    public class ProfileRepository : BaseRepository<Uprofile>, IProfileRepository
    {
        private readonly TimeTravelAgencyContext _db;

        public ProfileRepository(TimeTravelAgencyContext context) : base(context)
        {
            _db = context;
        }

        public async Task<Uprofile> GetById(int id)
        {
            return await _db.Uprofiles.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
