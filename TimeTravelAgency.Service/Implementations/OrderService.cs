using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTravelAgency.DAL.Interfaces;
using TimeTravelAgency.DAL.Repositories;
using TimeTravelAgency.Domain.Entity;
using TimeTravelAgency.Domain.Enum;
using TimeTravelAgency.Domain.Helpers;
using TimeTravelAgency.Domain.Response;
using TimeTravelAgency.Service.Interfaces;

namespace TimeTravelAgency.Service.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IBaseRepository<Order> _orderRepository;

        public OrderService(IBaseRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<IBaseResponse<Order>> AddTourToCart(Order order)
        {
            var baseResponse = new BaseResponse<Order>();
            try
            {
                await _orderRepository.Create(order);
                baseResponse.StatusCode = StatusCode.OK;

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<Order>()
                {
                    Description = $"[CreateOrder] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<bool>> CreateOrder(int userId)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                var temp_order = await _orderRepository.SelectOrder(userId, StatusOrder.InCart);
                if (temp_order == null)
                {
                    baseResponse.Description = "Корзина пуста!";
                    baseResponse.StatusCode = StatusCode.CartIsEmpty;

                    return baseResponse;
                }
                DateTime date = DateTime.Now;
                string number = GeneratorOrderNumber.GenerateNumber(temp_order.ElementAt(0).Id, date);

                foreach (var item in temp_order)
                {
                    item.Status = StatusOrder.Сreated;
                    item.DateCreate = date;
                    item.Number = number;
                }

                await _orderRepository.UpdateRange(temp_order);
                baseResponse.StatusCode = StatusCode.OK;

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[CreateOrder] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<bool>> DeleteOrderById(int id)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                var temp_order = await _orderRepository.GetById(id);
                if (temp_order == null)
                {
                    baseResponse.Description = "Обьект не найден";
                    baseResponse.StatusCode = StatusCode.ObjectNotFound;

                    return baseResponse;
                }
                await _orderRepository.Delete(temp_order);
                baseResponse.StatusCode = StatusCode.OK;

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[DeleteOrderById] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public Task<IBaseResponse<Order>> Edit(int id, Order order)
        {
            throw new NotImplementedException();
        }

        public Task<IBaseResponse<Order>> GetOrderById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IBaseResponse<IQueryable<ExtendedOrder>>> GetOrders(int userId)
        {
            var baseResponse = new BaseResponse<IQueryable<ExtendedOrder>>();
            try
            {
                var orders = await _orderRepository.SelectExtendedOrder(userId, StatusOrder.InCart);
                if (orders.Count() == 0)
                {
                    baseResponse.Description = "Найдено 0 элементов";
                    baseResponse.StatusCode = StatusCode.OK;
                    return baseResponse;
                }

                baseResponse.Data = orders;
                baseResponse.StatusCode = StatusCode.OK;

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<IQueryable<ExtendedOrder>>()
                {
                    Description = $"[GetOrders] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
