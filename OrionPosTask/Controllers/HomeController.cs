using Microsoft.AspNetCore.Mvc;
using OrionPosTask.Models;
using System.Diagnostics;

namespace OrionPosTask.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var oncekiDeger = HttpContext.Session.GetInt32("Gosterim");
            if (oncekiDeger == null) oncekiDeger = 0;

            HttpContext.Session.SetInt32("Gosterim", (int)oncekiDeger + 1);

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult ChangeTheme(string theme)
        {
            HttpContext.Session.SetString("Theme", theme);

            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}
