using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TimeTravelAgency.Service.Interfaces;

namespace TimeTravelAgency.Controllers
{
    public class CartController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly ITourService _tourService;

        public CartController(IOrderService orderService, ITourService tourService)
        {
            _orderService = orderService;
            _tourService = tourService;
        }

        [HttpPost, HttpGet]
        public async Task<IActionResult> GetCart(int userId)
        {
            var response = await _orderService.GetOrders(userId);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                if (response.Data != null)
                {
                    return View(response.Data.ToList());
                }
                return View();
            }
            return RedirectToAction("Error");
        }

        public async Task<IActionResult> DeleteTourFromCart(int id)
        {
            var response = await _orderService.DeleteOrderById(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("GetCart", new { userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value) });
            }
            return RedirectToAction("Error");
        }
    }
}
