using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TimeTravelAgency.Domain.Entity;
using TimeTravelAgency.Domain.Enum;
using TimeTravelAgency.Domain.ViewModels.Cart;
using TimeTravelAgency.Service.Interfaces;

namespace TimeTravelAgency.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly ITourService _tourService;
        private readonly IPictureService _pictureService;

        public CartController(IOrderService orderService, ITourService tourService, IPictureService pictureService)
        {
            _orderService = orderService;
            _tourService = tourService;
            _pictureService = pictureService;
        }

        [HttpPost, HttpGet]
        public async Task<IActionResult> GetCart(int userId)
        {
            var response = await _orderService.GetOrders(userId);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                var response_picture = await _pictureService.GetPictures(ViewName.Tour);
                if (response_picture.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    if (response.Data != null)
                    {
                        OrdersWithPicturesViewModel ordersWithPictures = new OrdersWithPicturesViewModel()
                        {
                            orders = response.Data.ToList(),
                            pictures = new Dictionary<string, Picture>()
                        };

                        if (response_picture.Data != null)
                            using (var pic = response_picture.Data.GetEnumerator())
                            {
                                while (pic.MoveNext())
                                {
                                    ordersWithPictures.pictures.Add(pic.Current.Title, pic.Current);
                                }

                            }
                        return View(ordersWithPictures);
                    }

                }
                return View();
            }
            return RedirectToAction("Error", "Shared");
        }

        public async Task<IActionResult> DeleteTourFromCart(int id)
        {
            var response = await _orderService.DeleteOrderById(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("GetCart", new { userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value) });
            }
            return RedirectToAction("Error", "Shared");
        }
    }
}
