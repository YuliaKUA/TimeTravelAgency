using Microsoft.AspNetCore.Mvc;
using TimeTravelAgency.Service.Interfaces;
using TimeTravelAgency.Domain.ViewModels.Profile;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TimeTravelAgency.Domain.Entity;
using TimeTravelAgency.Service.Implementations;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace TimeTravelAgency.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpGet]
        public async Task<IActionResult> EditProfile(int id)
        {
            var response = await _profileService.GetProfileById(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }

            return RedirectToAction("Error", "Shared");
        }
        [HttpPost]
        public async Task<IActionResult> EditProfile(int id, ProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                Uprofile profile = new Uprofile
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Age = model.Age,
                    Email = model.Email,
                    Phone = model.Phone,
                    Uaddress = model.Uaddress
                };

                var response = await _profileService.Edit(id, profile);
                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    return RedirectToAction("Index", "Home");
                }

            }
            return RedirectToAction("Error", "Shared");
        }
    }
}
