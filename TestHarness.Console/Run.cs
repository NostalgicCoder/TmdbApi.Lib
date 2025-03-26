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

            List<Int32> filmIds = new List<Int32>();
            filmIds.Add(1091);
            filmIds.Add(9532);
            filmIds.Add(9792);

            _tmdb.GetTmdbIdsThatMatchKeywordOrYearCriteria(filmIds, Caller.Film, "yes");

            List<Int32> actorIds = new List<Int32>();
            actorIds.Add(679);
            actorIds.Add(27811);
            actorIds.Add(1059597);

            _tmdb.GetTmdbIdsThatMatchKeywordOrYearCriteria(actorIds, Caller.Actor, "weller");

            _tmdb.GetTmdbIdsThatMatchKeywordOrYearCriteria(filmIds, Caller.Film, null, "1982");

            System.Console.ReadLine();
        }
    }
}