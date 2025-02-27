using TmdbApi.Lib;
using TmdbApi.Lib.Interfaces;

namespace TestHarness.Console
{
    public class Run
    {
        private ITmdb _tmdb;

        public Run()
        {
            _tmdb = new Tmdb("YOUR_TMDB_API_READACCESS_TOKEN_GOES_HERE");
        }

        public void CallApi()
        {
            _tmdb.GetConfigurationData();
            _tmdb.SearchForFilmTvPerson("Robocop");
            _tmdb.SearchForFilmAndCreditsById(5548);
            _tmdb.SearchForPersonById(2461);
            _tmdb.GetMovieImages(5548);
            _tmdb.MoviesNowPlaying();
            _tmdb.GetMovieGenreList();
            _tmdb.GetTvGenreList();

            System.Console.ReadLine();
        }
    }
}