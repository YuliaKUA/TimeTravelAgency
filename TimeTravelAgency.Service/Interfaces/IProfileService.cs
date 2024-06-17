using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTravelAgency.Domain.Entity;
using TimeTravelAgency.Domain.Response;
using TimeTravelAgency.Domain.ViewModels.Profile;

namespace TimeTravelAgency.Service.Interfaces
{
    public interface IProfileService
    {
        Task<IBaseResponse<bool>> CreateProfileById(int id);
        Task<IBaseResponse<bool>> DeleteProfileById(int id);
        Task<IBaseResponse<ProfileViewModel>> GetProfileById(int id);
        Task<IBaseResponse<IEnumerable<Uprofile>>> GetProfiles();
        Task<IBaseResponse<Uprofile>> Edit(int id, Uprofile profile);
        Task<IBaseResponse<bool>> AddRangeProfiles(IEnumerable<Uprofile> profiles);
    }
}
