using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTravelAgency.Domain.Entity;
using TimeTravelAgency.Domain.Enum;

namespace TimeTravelAgency.DAL.Interfaces
{
    public interface IPictureRepository : IBaseRepository<Picture>
    {
        Task<Picture> GetByTitle(string title);
        Task<Picture> GetById(int id);
        Task<List<Picture>> SelectPictures(ViewName viewName);
    }
}
