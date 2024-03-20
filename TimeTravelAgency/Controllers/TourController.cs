using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using TimeTravelAgency.Domain.Entity;
using TimeTravelAgency.Service.Interfaces;

namespace TimeTravelAgency.Controllers
{
    public class TourController : Controller
    {
        private readonly ITourService _tourService;

        public TourController(ITourService tourService)
        {
            _tourService = tourService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTours()
        {
            var response = await _tourService.GetTours();
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data.ToList());
            }

            return RedirectToAction("Error");
        }

        [Authorize(Roles = "Admin, Moderator")]
        public async Task<IActionResult> CreateTour()
        {
            return View();
        }
        [Authorize(Roles = "Admin, Moderator")]
        [HttpPost]
        public async Task<IActionResult> CreateTour(Tour tour)
        {
            var response = await _tourService.CreateTour(tour);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("GetTours");
            }

            return RedirectToAction("Error");
        }

        [Authorize(Roles = "Admin, Moderator")]
        [HttpGet]
        public async Task<IActionResult> UpdateTour(int id)
        {
            var response = await _tourService.GetTourById(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }

            return RedirectToAction("Error");

        }

        [Authorize(Roles = "Admin, Moderator")]
        [HttpPost]
        public async Task<IActionResult> UpdateTour(int id, Tour tour)
        {
            var response = await _tourService.Edit(id, tour);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("GetTours");
            }

            return RedirectToAction("Error");
        }

        [Authorize(Roles = "Admin, Moderator")]
        [HttpGet]
        public async Task<IActionResult> DeleteTour(int id)
        {
            var response = await _tourService.DeleteTourById(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("GetTours");
            }

            return RedirectToAction("Error");
        }

        public async Task<IActionResult> TourInfo(int id)
        {
            var response = await _tourService.GetTourById(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }

            return RedirectToAction("Error");
        }

    }
}
