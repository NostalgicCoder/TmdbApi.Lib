using MovieTvTracker.Web.Interfaces;
using MovieTvTracker.Web.Models;
using TmdbApi.Lib.Models;

namespace MovieTvTracker.Web.Class
{
    public class GetStatistics : IGetStatistics
    {
        /// <summary>
        /// Get the most popular years, genres of films for the user based on the database / TMDB API data provided
        /// - Could also have been done as a dictionary object but the below will be in a easier format for graphing / sorting
        /// </summary>
        /// <param name="media"></param>
        /// <returns></returns>
        public IMedia GetPopularYearsAndGenres(IMedia media)
        {
            foreach (WatchedMediaItem item in media.WatchedMediaResults.WatchedFilms)
            {
                try
                {
                    DateTime releaseYear = Convert.ToDateTime(item.ResultReturn.FilmIdResult.release_date);

                    if (media.Stats.FilmYears.Any(x => x.Year == releaseYear.Year))
                    {
                        media.Stats.FilmYears.Where(x => x.Year == releaseYear.Year).Select(x => x.Qty = x.Qty + 1).ToList();
                    }
                    else
                    {
                        media.Stats.FilmYears.Add(new FilmYear() { Year = releaseYear.Year, Qty = 1 });
                    }
                }
                catch (Exception ex)
                {
                    // Handle any bad data issues from conversion of 'release_date' to DateTime yet still process working resuls
                    // TODO: Add error handling
                }

                foreach (Genre genre in item.ResultReturn.FilmIdResult.genres)
                {
                    if (media.Stats.FilmGenres.Any(x => x.Genre == genre.name))
                    {
                        media.Stats.FilmGenres.Where(x => x.Genre == genre.name).Select(x => x.Qty = x.Qty + 1).ToList();
                    }
                    else
                    {
                        media.Stats.FilmGenres.Add(new FilmGenre() { Genre = genre.name, Qty = 1 });
                    }
                }
            }

            media.Stats.FilmYears = media.Stats.FilmYears.OrderBy(x => x.Year).ToList();
            media.Stats.FilmGenres = media.Stats.FilmGenres.OrderByDescending(x => x.Genre).ToList();

            return media;
        }
    }
}