using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TimeTravelAgency.Models;
using Microsoft.Win32;
using System.IO;
using System.Windows;
using TimeTravelAgency.Service.Interfaces;
using TimeTravelAgency.Domain.Entity;
using TimeTravelAgency.Service.Implementations;
using TimeTravelAgency.Domain.Enum;
using System.Web;

namespace TimeTravelAgency.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPictureService _pictureService;

        public HomeController(ILogger<HomeController> logger, IPictureService pictureService)
        {
            _logger = logger;
            _pictureService = pictureService;
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}

        public async Task<IActionResult> Index()
        {
            var response = await _pictureService.GetPictures(ViewName.Index);

            ///for test
            //return View(response.Data);      

            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                if (response.Data != null)
                {
                    return View(response.Data);
                }
                return View();
            }
            return RedirectToAction("Error");
        }

        [HttpPost]
        public async Task<IActionResult> CreatePicture(IFormFile uploadImage)
        {
            Picture pic = new Picture() { ViewName = ViewName.Index };
            if (ModelState.IsValid && uploadImage != null)
            {
                byte[] imageData = null;
                // считываем переданный файл в массив байтов
                using (var binaryReader = new BinaryReader(uploadImage.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)uploadImage.Length);
                }
                // установка массива байтов
                pic.Image = imageData;

                var response = await _pictureService.CreatePicture(pic);

                /// for test
                //return RedirectToAction("Index");

                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    return RedirectToAction("Index");
                }

            }
            return RedirectToAction("Error");
            //return View(pic);
        }

        public IActionResult Privacy()
        {
            return PartialView();
        }

        public IActionResult About()
        {
            return View("About");
        }

        public IActionResult TravelGuide()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}