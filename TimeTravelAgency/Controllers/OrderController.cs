using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TimeTravelAgency.Domain.Entity;
using TimeTravelAgency.Domain.Enum;
using TimeTravelAgency.Service.Implementations;
using TimeTravelAgency.Service.Interfaces;

namespace TimeTravelAgency.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly ITourService _tourService;

        public OrderController(IOrderService orderService, ITourService tourService)
        {
            _orderService = orderService;
            _tourService = tourService;
        }
        public async Task<IActionResult> CreateOrder(int tourId, int userId)
        {
            Order order = new Order
            {
                TourId = tourId,
                UserId = userId,
                DateCreate = DateTime.Now,
                Status = StatusOrder.Сreated
            };

            var response = await _orderService.CreateOrder(order);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Error");
        }
        [HttpPost, HttpGet]
        public async Task<IActionResult> GetBasket(int userId)
        {
            var response = await _orderService.GetOrders(userId);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data.ToList());
            }
            return RedirectToAction("Error");
        }

        public async Task<IActionResult> DeleteOrder(int id)
        {
            var response = await _orderService.DeleteOrderById(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("GetBasket", new { userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value) });
            }
            return RedirectToAction("Error");
        }

    }
}
