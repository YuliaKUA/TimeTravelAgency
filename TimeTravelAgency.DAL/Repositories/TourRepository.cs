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
    public class TourRepository : BaseRepository<Tour>, ITourRepository
    {
        private readonly TimeTravelAgencyContext _db;

        public TourRepository(TimeTravelAgencyContext context) : base(context)
        {
            _db = context;
        }

        public async Task<Tour> GetById(int id)
        {
            return await _db.Tours.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Tour> GetByName(string name)
        {
            return await _db.Tours.FirstOrDefaultAsync(x => x.Title == name);
        }

    }
}
