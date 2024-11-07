using Microsoft.AspNetCore.Mvc;
using TmdbApi.Lib;
using MovieTvTracker.Web.Models;
using MovieTvTracker.Web.Data;
using TmdbApi.Lib.Interfaces;

namespace MovieTvTracker.Web.Controllers
{
    public class MovieController : Controller
    {
        private ITmdb _tmdb = new Tmdb();

        private ErrorViewModel _errorViewModel;

        private readonly ApplicationDbContext _db;

        /// <summary>
        /// Constructor
        /// </summary>
        public MovieController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(string keyword)
        {
            Media media = new Media();

            media.TMDBData = _tmdb.SearchForFilmTvPerson(keyword);

            return View(media);
        }

        [HttpGet]
        public IActionResult MoviesNowPlaying()
        {
            Media media = new Media();

            media.TMDBData = _tmdb.MoviesNowPlaying();

            return View(media);
        }

        /// <summary>
        /// Aquire watched media (film/tv) results from the database, run those results through TMDB API to get information and then feed that to the model that supplies the view.
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpGet]
        public IActionResult GetWatchedMedia()
        {
            Media media = new Media();
            WatchedMediaResults watchedMediaResults = new WatchedMediaResults();

            foreach (WatchedMedia item in _db.WatchedMedia.OrderByDescending(x => x.LastWatched))
            {
                WatchedMediaItem watchedMediaItem = new WatchedMediaItem();

                watchedMediaItem.WatchedMedia = item;

                switch (item.ContentType)
                {
                    case "Film":
                        watchedMediaItem.ResultReturn = _tmdb.SearchForFilmAndCreditsById(item.TMDBId);
                        watchedMediaResults.WatchedFilms.Add(watchedMediaItem);
                        break;
                    case "TV":
                        watchedMediaItem.ResultReturn = _tmdb.SearchForTvAndCreditsById(item.TMDBId);
                        watchedMediaResults.WatchedTV.Add(watchedMediaItem);
                        break;
                }
            }

            media.WatchedMediaResults = watchedMediaResults;

            return View(media);
        }

        /// <summary>
        /// Aquire favorite actor results from the database, run those results through TMDB API to get information and then feed that to the model that supplies the view.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetFavoriteActors()
        {
            Media media = new Media();

            foreach (FavoriteActor item in _db.FavoriteActor)
            {
                try
                {
                    media.FavoriteActorResults.Add(_tmdb.SearchForPersonAndCreditsById(item.TMDBId));
                }
                catch(Exception ex)
                {
                    // TODO: Add error handling
                }
            }

            return View(media);
        }

        /// <summary>
        /// Check if the selected TMDB ID exists in the database, if a result already exists update minimal req fields otherwise create a brand new 'WatchedMedia' entity result in the database.
        /// </summary>
        /// <param name="media"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkAsWatched(Media media)
        {
            try
            {
                WatchedMedia watchedMedia = _db.WatchedMedia.Where(x => x.TMDBId == media.SelectedTMDBId).FirstOrDefault();

                if (watchedMedia == null)
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

                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // TODO: Add error handling
            }

            return RedirectToAction("MoviesNowPlaying", "Movie");
        }

        /// <summary>
        /// Check if the selected TMDB ID exists in the database, if a result doesnt already exist then create a 'FavoriteActor' entity and add it to the database
        /// </summary>
        /// <param name="media"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkFavoriteActor(Media media)
        {
            try
            {
                FavoriteActor favoriteActor = _db.FavoriteActor.Where(x => x.TMDBId == media.TMDBData.PersonIdResult.id).FirstOrDefault();

                if (favoriteActor == null)
                {
                    favoriteActor = new FavoriteActor()
                    {
                        TMDBId = media.TMDBData.PersonIdResult.id
                    };

                    await _db.FavoriteActor.AddAsync(favoriteActor);
                    await _db.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                // TODO: Add error handling
            }

            return RedirectToAction("MoviesNowPlaying", "Movie");
        }

        [HttpGet]
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

        [HttpGet]
        public IActionResult GetUserTVSelectionById(int id)
        {
            Media media = new Media();

            media.SelectedTMDBId = id;
            media.TMDBData = _tmdb.SearchForTvAndCreditsById(id);

            if (media.TMDBData.TvIdResult != null)
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

        [HttpGet]
        public IActionResult GetUserPersonSelectionById(int id)
        {
            Media media = new Media();

            media.SelectedTMDBId = id;
            media.TMDBData = _tmdb.SearchForPersonAndCreditsById(id);

            if (media.TMDBData.PersonIdResult != null)
            {
                return View(media);
            }
            else
            {
                _errorViewModel = new ErrorViewModel();
                _errorViewModel.Error = "PersonIdResult has been returned NULL.";

                return View("Error", _errorViewModel);
            }
        }
    }
}