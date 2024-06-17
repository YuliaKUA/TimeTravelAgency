using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using TimeTravelAgency.Domain.Entity;
using TimeTravelAgency.Domain.Enum;
using TimeTravelAgency.Domain.ViewModels.TourViewModels;
using TimeTravelAgency.Service.Interfaces;

namespace TimeTravelAgency.Controllers
{
    public class TourController : Controller
    {
        private readonly ITourService _tourService;
        private readonly IPictureService _pictureService;

        public TourController(ITourService tourService, IPictureService pictureService)
        {
            _tourService = tourService;
            _pictureService = pictureService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTours()
        {
            var response = await _tourService.GetTours();
            var response_picture = await _pictureService.GetPictures(ViewName.Tour);

            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                if (response_picture.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    ToursWithPicturesViewModel tourWithPictures = new ToursWithPicturesViewModel()
                    {
                        tours = response.Data.ToList(),
                        pictures = new Dictionary<string, Picture>()
                    };

                    if (response_picture.Data != null)
                        using (var pic = response_picture.Data.GetEnumerator())
                        {
                            while (pic.MoveNext())
                            {
                                tourWithPictures.pictures.Add(pic.Current.Title, pic.Current);
                            }

                        }

                    return View(tourWithPictures);
                }

            }

            return RedirectToAction("Error", "Shared");
        }
        [Authorize(Roles = "Admin, Moderator")]
        public async Task<IActionResult> CreateTour()
        {
            return PartialView();
        }

        private void setByteArray(Picture pic, in IFormFile uploadImage)
        {
            if (uploadImage != null)
            {
                byte[] imageData = null;
                // считываем переданный файл в массив байтов
                using (var binaryReader = new BinaryReader(uploadImage.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)uploadImage.Length);
                }
                // установка массива байтов
                pic.Image = imageData;
            }
        }

        [Authorize(Roles = "Admin, Moderator")]
        [HttpPost]
        public async Task<IActionResult> CreateTour(Tour tour, IFormFile uploadImage)
        {
            var response = await _tourService.CreateTour(tour);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                Picture pic = new Picture() { ViewName = ViewName.Tour, Title = string.Format("{0}-main", tour.Id) };

                if (uploadImage != null)
                {
                    setByteArray(pic, uploadImage);

                    //ViewBag.Picture
                }

                var response_pic = await _pictureService.CreatePicture(pic);
                if (response_pic.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    return RedirectToAction("GetTours");
                }

            }

            return RedirectToAction("Error", "Shared");
        }

        [Authorize(Roles = "Admin, Moderator")]
        [HttpGet]
        public async Task<IActionResult> UpdateTour(int id)
        {
            var response = await _tourService.GetTourById(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return PartialView(response.Data);
            }

            return RedirectToAction("Error", "Shared");

        }

        [Authorize(Roles = "Admin, Moderator")]
        [HttpPost]
        public async Task<IActionResult> UpdateTour(int id, Tour tour, IFormFile uploadImage)
        {
            var response = await _tourService.Edit(id, tour);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                Picture pic = new Picture() { ViewName = ViewName.Tour, Title = string.Format("{0}-main", tour.Id) };
                if (uploadImage != null)
                {
                    setByteArray(pic, uploadImage);

                    //ViewBag.Picture
                    var response2 = await _tourService.GetTourById(id);
                    var response_pic = await _pictureService.GetPictureByTitle(string.Format("{0}-main", response2.Data.Id));
                    if (response_pic.StatusCode == Domain.Enum.StatusCode.OK)
                    {
                        pic.Title = response_pic.Data.Title;
                        var response_pic2 = await _pictureService.Edit(response_pic.Data.Id, pic);
                        if (response_pic2.StatusCode == Domain.Enum.StatusCode.OK)
                        {
                            return RedirectToAction("GetTours");
                        }
                    }
                }
                return RedirectToAction("GetTours");
            }

            return RedirectToAction("Error", "Shared");
        }

        [Authorize(Roles = "Admin, Moderator")]
        [HttpGet]
        public async Task<IActionResult> DeleteTour(int id)
        {
            var response = await _tourService.GetTourById(id);
            var response2 = await _tourService.DeleteTourById(id);
            if (response2.StatusCode == Domain.Enum.StatusCode.OK)
            {
                var response_pic = await _pictureService.DeletePictureByTitle(string.Format("{0}-main", response.Data.Id));
                if (response_pic.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    return RedirectToAction("GetTours");
                }
            }
            return RedirectToAction("Error", "Shared");
        }

        public async Task<IActionResult> TourInfo(int id)
        {
            var response = await _tourService.GetTourById(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                TourWithPicturesViewModel tourWithPictures = new TourWithPicturesViewModel()
                {
                    tour = response.Data,
                    pictures = new Dictionary<string, Picture>()
                };

                var response_pic = await _pictureService.GetPictures(ViewName.Tour);
                if (response_pic.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    if (response_pic.Data != null)
                        using (var pic = response_pic.Data.GetEnumerator())
                        {
                            while (pic.MoveNext())
                            {
                                tourWithPictures.pictures.Add(pic.Current.Title, pic.Current);
                            }

                        }

                    return View(tourWithPictures);
                }

            }

            return RedirectToAction("Error", "Shared");
        }

    }
}
