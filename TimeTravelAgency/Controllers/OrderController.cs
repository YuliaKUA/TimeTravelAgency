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
        public async Task<IActionResult> AddTourToCart(int tourId, int userId)
        {
            Order order = new Order
            {
                TourId = tourId,
                UserId = userId,
                DateCreate = DateTime.Now,
                Status = StatusOrder.InCart
            };

            var response = await _orderService.AddTourToCart(order);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("GetCart", "Cart", new { userId = userId });
            }

            return RedirectToAction("Error", "Shared");
        }

        public async Task<IActionResult> CreateOrder(int userId)
        {
            var response = await _orderService.CreateOrder(userId);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Error", "Shared");
        }

    }
}
