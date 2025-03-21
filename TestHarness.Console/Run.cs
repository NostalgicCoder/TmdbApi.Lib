using TmdbApi.Lib;
using TmdbApi.Lib.Enum;
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
            _tmdb.SearchForPersonAndCreditsById(1245); // Scarlett Johansson
            _tmdb.SearchForPersonAndCreditsById(500); // Tom Cruise
            _tmdb.SearchForPersonAndCreditsById(190); // Clint Eastwood

            List<Int32> ids = new List<Int32>();
            ids.Add(1091);
            ids.Add(9532);
            ids.Add(9792);

            _tmdb.ConvertIdToTitleAndCheckForKeywordMatch(ids, "yes", Caller.Film);

            System.Console.ReadLine();
        }
    }
}