using Microsoft.AspNetCore.Mvc;
using NLearner.Domain.Entities;
using NLearner.Infrastructure.Service;
using NLearner.ViewModels;
using System.Security.Principal;

namespace NLearner.Controllers
{
    public class UserController : Controller
    {
        private readonly IIdentityService _identityService;
        public UserController(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await _identityService.LoginAsync(model.Email, model.Password);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Invalid Login Attemp.");
                return View(model);
            }
            var user = _identityService.FindByEmailAsync(model.Email);
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = new DTO.Users.RegisterAddRequest()
            {
                Email = model.Email,
                UserName = model.Email
            };
            var result = await _identityService.RegisterAsync(user, model.Password);
            if (result.Succeeded)
            {
                RedirectToAction("Index", "Home");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _identityService.LogoutAsync();
            return LocalRedirect("~/");
        }
    }
}
