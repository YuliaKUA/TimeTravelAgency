using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTravelAgency.Domain.Entity;
using TimeTravelAgency.Domain.Response;

namespace TimeTravelAgency.Service.Interfaces
{
    public interface IOrderService
    {
        Task<IBaseResponse<Order>> CreateOrder(Order order);
        Task<IBaseResponse<bool>> DeleteOrderById(int id);
        Task<IBaseResponse<Order>> GetOrderById(int id);
        Task<IBaseResponse<IQueryable<ExtendedOrder>>> GetOrders(int userId);
        Task<IBaseResponse<Order>> Edit(int id, Order order);
    }
}
