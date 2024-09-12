using Microsoft.AspNetCore.Mvc;
using TmdbApi.Lib.Models;
using TmdbApi.Lib;
using TestHarness.Visual.Models;

namespace TestHarness.Visual.Controllers
{
    public class MovieController : Controller
    {
        private Tmdb _tmdb = new Tmdb();
        private ErrorViewModel _errorViewModel;

        public IActionResult Index(string keyword)
        {
            ResultReturn result = _tmdb.SearchForFilmTV(keyword);

            return View(result);
        }

        public IActionResult GetUserFilmSelectionById(int id)
        {
            ResultReturn result = _tmdb.SearchForFilmById(id);

            if (result.FilmIdResult != null)
            {
                return View(result);
            }
            else
            {
                _errorViewModel = new ErrorViewModel();
                _errorViewModel.Error = "FilmIdResult has been returned NULL.";

                return View("Error", _errorViewModel);
            }
        }

        public IActionResult GetUserTVSelectionById(int id)
        {
            ResultReturn result = _tmdb.SearchForTVById(id);

            if(result.TVIdResult != null)
            {
                return View(result);
            }
            else
            {
                _errorViewModel = new ErrorViewModel();
                _errorViewModel.Error = "TVIdResult has been returned NULL.";

                return View("Error", _errorViewModel);
            }
        }

        public IActionResult MoviesNowPlaying()
        {
            ResultReturn result = _tmdb.MoviesNowPlaying();

            return View(result);
        }
    }
}