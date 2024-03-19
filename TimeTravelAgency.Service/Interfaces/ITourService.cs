using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTravelAgency.Domain.Response;
using TimeTravelAgency.Domain.Entity;
using System.Runtime.ConstrainedExecution;

namespace TimeTravelAgency.Service.Interfaces
{
    public interface ITourService
    {
        Task<IBaseResponse<Tour>> CreateTour(Tour tour);
        Task<IBaseResponse<bool>> DeleteTourById(int id);
        Task<IBaseResponse<Tour>> GetTourById(int id);

        Task<IBaseResponse<Tour>> GetTourByTitle(string title);
        Task<IBaseResponse<IEnumerable<Tour>>> GetTours();
        Task<IBaseResponse<Tour>> Edit(int id, Tour tour);
        Task<IBaseResponse<Tour>> Edit(string name, Tour tour);
    }
}
