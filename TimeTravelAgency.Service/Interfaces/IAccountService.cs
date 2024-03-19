using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TimeTravelAgency.Domain.Entity;
using TimeTravelAgency.Domain.Response;
using TimeTravelAgency.Domain.ViewModels.Account;

namespace TimeTravelAgency.Service.Interfaces
{
    public interface IAccountService
    {
        Task<BaseResponse<ClaimsIdentity>> Register(RegisterViewModel model);

        Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel model);

        //Task<BaseResponse<bool>> ChangePassword(ChangePasswordViewModel model);

        Task<IBaseResponse<User>> CreateUser(User user);
        Task<IBaseResponse<bool>> DeleteUserById(int id);
        Task<IBaseResponse<User>> GetUserById(int id);
        Task<IBaseResponse<User>> GetUserByLogin(string login);
        Task<IBaseResponse<IEnumerable<User>>> GetUsers();
        Task<IBaseResponse<User>> Edit(int id, User user);
    }
}
