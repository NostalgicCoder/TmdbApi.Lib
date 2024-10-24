using Microsoft.AspNetCore.Mvc;
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
            Media media = new Media();

            media.TMDBData = _tmdb.SearchForFilmTV(keyword);

            return View(media);
        }

        public IActionResult MoviesNowPlaying()
        {
            Media media = new Media();

            media.TMDBData = _tmdb.MoviesNowPlaying();

            return View(media);
        }

        public IActionResult MarkAsWatched(Media media)
        {
            return RedirectToAction("MoviesNowPlaying", "Movie");
        }

        public IActionResult GetUserFilmSelectionById(int id)
        {
            Media media = new Media();

            media.Id = id;
            media.TMDBData = _tmdb.SearchForFilmAndCreditsById(id);

            if (media.TMDBData.FilmIdResult != null && media.TMDBData.CreditsByFilmId != null)
            {
                return View(media);
            }
            else
            {
                _errorViewModel = new ErrorViewModel();
                _errorViewModel.Error = "FilmIdResult or CreditsByFilmId has been returned NULL.";

                return View("Error", _errorViewModel);
            }
        }

        public IActionResult GetUserTVSelectionById(int id)
        {
            Media media = new Media();

            media.Id = id;
            media.TMDBData = _tmdb.SearchForTVById(id);

            if(media.TMDBData.TVIdResult != null)
            {
                return View(media);
            }
            else
            {
                _errorViewModel = new ErrorViewModel();
                _errorViewModel.Error = "TVIdResult has been returned NULL.";

                return View("Error", _errorViewModel);
            }
        }
    }
}