using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTravelAgency.DAL.Repositories;
using TimeTravelAgency.Domain.Entity;
using TimeTravelAgency.Domain.Enum;

namespace TimeTravelAgency.DAL.Interfaces
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        Task<Order> GetById(int id);
        Task<IQueryable<ExtendedOrder>> SelectExtendedOrder(int userId, StatusOrder status);
        Task<IEnumerable<Order>> SelectOrder(int userId, StatusOrder status);
    }
}
