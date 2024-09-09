using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TestHarness.Visual.Models;
using TmdbApi.Lib;
using TmdbApi.Lib.Models;

namespace TestHarness.Visual.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(string keyword)
        {
            Tmdb tmdb = new Tmdb();

            tmdb.GetConfigurationData();

            ResultReturn result = tmdb.SearchForFilmTV(keyword);

            return View(result);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
