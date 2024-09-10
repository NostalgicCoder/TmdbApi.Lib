using Microsoft.AspNetCore.Mvc;
using TmdbApi.Lib.Models;
using TmdbApi.Lib;

namespace TestHarness.Visual.Controllers
{
    public class MovieController : Controller
    {
        private Tmdb _tmdb = new Tmdb();

        public IActionResult Index(string keyword)
        {
            ResultReturn result = _tmdb.SearchForFilmTV(keyword);

            return View(result);
        }

        public IActionResult GetUserSelection(int id)
        {
            ResultReturn result = _tmdb.SearchForFilmById(id);

            return View(result);
        }
    }
}