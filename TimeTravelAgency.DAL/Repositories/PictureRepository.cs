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
    public class PictureRepository : BaseRepository<Picture>, IPictureRepository
    {
        private readonly TimeTravelAgencyContext _db;

        public PictureRepository(TimeTravelAgencyContext context) : base(context)
        {
            _db = context;
        }

        public async Task<Picture> GetById(int id)
        {
            return await _db.Pictures.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Picture> GetByTitle(string title)
        {
            return await _db.Pictures.FirstOrDefaultAsync(x => x.Title == title);
        }

        public async Task<IEnumerable<Picture>> SelectPictures(ViewName viewName)
        {
            var pictures = await (from p in _db.Pictures
                                  where p.ViewName == viewName || p.ViewName == ViewName.Error
                                  select new Picture
                                  {
                                      Id = p.Id,
                                      ViewName = p.ViewName,
                                      Title = p.Title,
                                      Href = p.Href,
                                      Image = p.Image,
                                  }).ToListAsync();

            return pictures.AsEnumerable();
        }
    }
}
