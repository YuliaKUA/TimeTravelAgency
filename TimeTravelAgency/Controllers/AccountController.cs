using Microsoft.AspNetCore.Mvc;
using TimeTravelAgency.Service.Interfaces;
using TimeTravelAgency.Domain.Entity;
using TimeTravelAgency.Service.Implementations;
using TimeTravelAgency.Domain.ViewModels.Account;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using TimeTravelAgency.Domain.Helpers;

namespace TimeTravelAgency.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IProfileService _profileService;

        public AccountController(IAccountService accountService, IProfileService profileService)
        {
            _accountService = accountService;
            _profileService = profileService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var response = await _accountService.GetUsers();
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data.ToList());
            }

            return RedirectToAction("Error", "Shared");
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _accountService.Register(model);
                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    var baseResponse = await _accountService.GetUserByLogin(model.Login);

                    await _profileService.CreateProfileById(baseResponse.Data.Id);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(response.Data));

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", response.Description);
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _accountService.Login(model);
                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(response.Data));

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", response.Description);
            }
            return View(model);
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "Admin, Moderator")]
        [HttpGet]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var response_profile = await _profileService.DeleteProfileById(id);
            if (response_profile.StatusCode == Domain.Enum.StatusCode.OK)
            {
                var response_account = await _accountService.DeleteUserById(id);
                if (response_account.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    return RedirectToAction("GetUsers");

                }
            }

            return RedirectToAction("Error", "Shared");
        }

        [Authorize(Roles = "Admin, Moderator")]
        [HttpGet]
        public async Task<IActionResult> UpdateUser(int id)
        {
            var response_user = await _accountService.GetUserById(id);
            var response_profile = await _profileService.GetProfileById(id);

            if (response_user.StatusCode == Domain.Enum.StatusCode.OK && response_profile.StatusCode == Domain.Enum.StatusCode.OK)
            {
                AccountViewModel account = new AccountViewModel
                {
                    ULogin = response_user.Data.ULogin,
                    HashPassword = null,
                    URole = response_user.Data.URole,
                    FirstName = response_profile.Data.FirstName,
                    LastName = response_profile.Data.LastName,
                    Age = response_profile.Data.Age,
                    Email = response_profile.Data.Email,
                    Phone = response_profile.Data.Phone,
                    Uaddress = response_profile.Data.Uaddress
                };

                return View(account);
            }

            return RedirectToAction("Error", "Shared");
        }

        [Authorize(Roles = "Admin, Moderator")]
        [HttpPost]
        public async Task<IActionResult> UpdateUser(int id, AccountViewModel account)
        {
            User user = new User
            {
                ULogin = account.ULogin,
                HashPassword = HashPasswordHelper.HashPassowrd(account.HashPassword),
                URole = account.URole
            };
            Uprofile profile = new Uprofile
            {
                FirstName = account.FirstName,
                LastName = account.LastName,
                Age = account.Age,
                Email = account.Email,
                Phone = account.Phone,
                Uaddress = account.Uaddress
            };
            var response_edit_user = await _accountService.Edit(id, user);
            var response_edit_profile = await _profileService.Edit(id, profile);
            if (response_edit_user.StatusCode == Domain.Enum.StatusCode.OK && response_edit_profile.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("GetUsers");
            }
            return RedirectToAction("Error", "Shared");
        }
    }
}
