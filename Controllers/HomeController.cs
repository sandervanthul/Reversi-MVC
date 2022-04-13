using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ReversiMvcApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using ReversiMvcApp.Migrations;
using ReversiMvcApp.Services;

namespace ReversiMvcApp.Controllers
{
    //[Authorize (Policy = "SpelerNietInSpelPolicy")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IService<Spel> _service;

        public HomeController(ILogger<HomeController> logger, IService<Spel> service)
        {
            _logger = logger;
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            ClaimsPrincipal currentUser = this.User;
            var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // Haal het spel
            var response = await _service.GetAsync(currentUserID, "/api/Speler");

            if (response.Token != null) return RedirectToAction("Play", nameof(Spellen), new {id = response.Token});  
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Privacy()
        {
            ClaimsPrincipal currentUser = this.User;
            var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // Haal het spel
            var response = await _service.GetAsync(currentUserID, "/api/Speler");

            if (response.Token != null) return RedirectToAction("Play", nameof(Spellen), new {id = response.Token});  

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
