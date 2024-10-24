using Microsoft.AspNetCore.Mvc;
using TmdbApi.Lib;
using TestHarness.Visual.Models;
using TestHarness.Visual.Data;

namespace TestHarness.Visual.Controllers
{
    public class MovieController : Controller
    {
        private Tmdb _tmdb = new Tmdb();
        private ErrorViewModel _errorViewModel;

        private readonly ApplicationDbContext _db;

        /// <summary>
        /// Constructor
        /// </summary>
        public MovieController(ApplicationDbContext db)
        {
            _db = db;
        }

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

        public async Task<IActionResult> MarkAsWatched(Media media)
        {
            WatchedMedia watchedMedia = _db.WatchedMedia.Where(x => x.TMDBId == media.SelectedTMDBId).FirstOrDefault();

            if(watchedMedia == null)
            {
                watchedMedia = new WatchedMedia();

                watchedMedia.TMDBId = media.SelectedTMDBId;
                watchedMedia.ContentType = media.SelectedContentType;
                watchedMedia.WatchedQty = 1;
                watchedMedia.FirstWatched = DateTime.Now;
                watchedMedia.LastWatched = DateTime.Now;

                await _db.WatchedMedia.AddAsync(watchedMedia);
            }
            else
            {
                watchedMedia.LastWatched = DateTime.Now;
                watchedMedia.WatchedQty = watchedMedia.WatchedQty + 1;

                _db.WatchedMedia.Update(watchedMedia);
            }

            if (ModelState.IsValid)
            {
                await _db.SaveChangesAsync();
            }

            return RedirectToAction("MoviesNowPlaying", "Movie");
        }

        public IActionResult GetUserFilmSelectionById(int id)
        {
            Media media = new Media();

            media.SelectedTMDBId = id;
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

            media.SelectedTMDBId = id;
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