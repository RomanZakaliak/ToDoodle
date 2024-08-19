using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Todo.Models;
using Todo.Services;
using Todo.ViewModels;

namespace Todo.Controllers
{
    public class HomeController(ILogger<HomeController> logger) : Controller
    {

        public IActionResult Index()
        {
            var homeViewModel = new HomeViewModel
            {
                IsAuthorized = this.User.Identity.IsAuthenticated,
            };

            return View(homeViewModel);
        }

        public IActionResult AccessDenied() => View();

        public IActionResult Privacy() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() =>
            View(new ErrorViewModel 
            { 
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier 
            });
    }
}
