﻿using MovieTvTracker.Web.Interfaces;
using MovieTvTracker.Web.Models;
using TmdbApi.Lib.Models;

namespace MovieTvTracker.Web.Class
{
    public class GetStatistics : IGetStatistics
    {
        /// <summary>
        /// Collect data on the release year, genre of each film title and year range watched and save it to the 'stats' object for consumption later.
        /// </summary>
        /// <param name="media"></param>
        /// <returns></returns>
        public IMedia GetFilmYearsRangeAndGenres(IMedia media)
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
                        media.Stats.FilmGenres.Add(new ContentGenre() { Genre = genre.name, Qty = 1 });
                    }
                }
            }

            media.Stats.FilmYears = media.Stats.FilmYears.OrderBy(x => x.Year).ToList();
            media.Stats.FilmGenres = media.Stats.FilmGenres.OrderByDescending(x => x.Qty).ToList();
            media.YearRange = string.Format("{0} > {1}", media.Stats.FilmYears.First().Year.ToString(), media.Stats.FilmYears.Last().Year.ToString());

            return media;
        }

        /// <summary>
        /// Collect data on the genre of each TV title watched and save it to the 'stats' object for consumption later.
        /// </summary>
        /// <param name="media"></param>
        /// <returns></returns>
        public IMedia GetTvGenres(IMedia media)
        {
            foreach (WatchedMediaItem item in media.WatchedMediaResults.WatchedTV)
            {
                foreach (Genre genre in item.ResultReturn.TvIdResult.genres)
                {
                    if (media.Stats.TvGenres.Any(x => x.Genre == genre.name))
                    {
                        media.Stats.TvGenres.Where(x => x.Genre == genre.name).Select(x => x.Qty = x.Qty + 1).ToList();
                    }
                    else
                    {
                        media.Stats.TvGenres.Add(new ContentGenre() { Genre = genre.name, Qty = 1 });
                    }
                }
            }

            media.Stats.TvGenres = media.Stats.TvGenres.OrderByDescending(x => x.Qty).ToList();

            return media;
        }

        /// <summary>
        /// Perform queries on the watched media data to aquire stats (figures) on number of films, tv watched for specific periods of time.  Save data back to the 'media' object.
        /// </summary>
        /// <param name="media"></param>
        /// <returns></returns>
        public IMedia GetQtyViewingStats(IMedia media)
        {
            media.FilmsLastMonth = 0;
            media.TvLastMonth = 0;

            if (DateTime.Now.Month == 1)
            {
                media.FilmsLastMonth = media.WatchedMediaResults.WatchedFilms.Where(item => item.WatchedMedia.LastWatched.Month == 12 && item.WatchedMedia.LastWatched.Year == (DateTime.Now.Year - 1)).Count();
            }
            else
            {
                media.FilmsLastMonth = media.WatchedMediaResults.WatchedFilms.Where(item => item.WatchedMedia.LastWatched.Month == (DateTime.Now.Month - 1)).Count();
            }

            if (DateTime.Now.Month == 1)
            {
                media.TvLastMonth = media.WatchedMediaResults.WatchedTV.Where(item => item.WatchedMedia.LastWatched.Month == 12 && item.WatchedMedia.LastWatched.Year == (DateTime.Now.Year - 1)).Count();
            }
            else
            {
                media.TvLastMonth = media.WatchedMediaResults.WatchedTV.Where(item => item.WatchedMedia.LastWatched.Month == (DateTime.Now.Month - 1)).Count();
            }

            media.FilmsThisYear = media.WatchedMediaResults.WatchedFilms.Where(item => item.WatchedMedia.LastWatched.Year == DateTime.Now.Year).Count();
            media.TvThisYear = media.WatchedMediaResults.WatchedTV.Where(item => item.WatchedMedia.LastWatched.Year == DateTime.Now.Year).Count();
            media.FilmsThisMonth = media.WatchedMediaResults.WatchedFilms.Where(item => item.WatchedMedia.LastWatched.Month == DateTime.Now.Month).Count();
            media.TvThisMonth = media.WatchedMediaResults.WatchedTV.Where(item => item.WatchedMedia.LastWatched.Month == DateTime.Now.Month).Count();

            return media;
        }
    }
}