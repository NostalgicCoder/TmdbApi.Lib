using TmdbApi.Lib;
using TmdbApi.Lib.Interfaces;

namespace TestHarness.Console
{
    public class Run
    {
        public void CallApi()
        {
            ITmdb tmdb = new Tmdb("PRIVATE");

            tmdb.GetConfigurationData();
            tmdb.SearchForFilmTvPerson("Robocop");
            tmdb.SearchForFilmAndCreditsById(5548);
            tmdb.SearchForPersonById(2461);
            tmdb.GetMovieImages(5548);
            tmdb.MoviesNowPlaying();
            tmdb.GetMovieGenreList();
            tmdb.GetTvGenreList();

            System.Console.ReadLine();
        }
    }
}