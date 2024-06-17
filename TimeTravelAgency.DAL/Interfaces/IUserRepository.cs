using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTravelAgency.Domain.Entity;
using TimeTravelAgency.Domain.Enum;
using TimeTravelAgency.Domain.ViewModels.Account;

namespace TimeTravelAgency.DAL.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> GetById(int id);
        Task<User> GetByName(string name);
        Task<List<AccountViewModel>> SelectFullAccount(int id);
    }
}
