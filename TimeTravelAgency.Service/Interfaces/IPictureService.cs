using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTravelAgency.Domain.Entity;
using TimeTravelAgency.Domain.Enum;
using TimeTravelAgency.Domain.Response;

namespace TimeTravelAgency.Service.Interfaces
{
    public interface IPictureService
    {
        Task<IBaseResponse<Picture>> CreatePicture(Picture picture);
        Task<IBaseResponse<bool>> DeletePictureByTitle(string title);
        Task<IBaseResponse<Picture>> GetPictureByTitle(string title);
        Task<IBaseResponse<List<Picture>>> GetPictures(ViewName viewName);
        Task<IBaseResponse<Picture>> Edit(int id, Picture picture);
    }
}
