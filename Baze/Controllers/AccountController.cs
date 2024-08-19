using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Todo.Models;
using Todo.Services;
using Todo.Services.Interfaces;
using Todo.ViewModels;

namespace Todo.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly INotificationService _notificationService;

        public AccountController(
            UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager,
            INotificationService notificationService)
        {
            userManager = userManager;
            _signInManager = signInManager;
            _notificationService = notificationService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel regModel)
        {
            if(ModelState.IsValid)
            {
                ApplicationUser user = new() { Email = regModel.Email, UserName = regModel.Email };

                var result = await userManager.CreateAsync(user, regModel.Password);

                if(result.Succeeded)
                {
                    var code = await userManager.GenerateEmailConfirmationTokenAsync(user);

                    var callbackUrl = Url.Action(
                        "ConfirmEmail", "Account", new { userId = user.Id, code},
                        protocol: HttpContext.Request.Scheme);

                    await _notificationService.SendNotificationAsync(regModel.Email, "Confirm your account",
                        $"Confirm your registration <a href='{callbackUrl}'>HERE</a>");

                    ViewBag.PopupData = $"Email has been sent to {regModel.Email}.";
                    return View();
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(regModel);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if(userId == null || code == null)
            {
                return View("Error");
            }

            var user = await userManager.FindByIdAsync(userId);
            if(user == null)
            {
                return View("Error");
            }

            var result = await userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
                return View();
            else
                return View("Error");
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if(ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(model.Email);
                if(user != null)
                {
                    if(!await userManager.IsEmailConfirmedAsync(user))
                    {
                        ModelState.AddModelError(string.Empty, "You do not confirm your email!");
                        return View(model);
                    }
                }

                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if(result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Incorrect email or password!");
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
