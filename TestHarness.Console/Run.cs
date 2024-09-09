using TmdbApi.Lib;

namespace TestHarness.Console
{
    public class Run
    {
        public void CallApi()
        {
            Tmdb tmdb = new Tmdb();

            tmdb.GetConfigurationData();

            tmdb.SearchForFilmTV("Robocop");

            tmdb.GetMovieImages(5548);

            System.Console.ReadLine();
        }
    }
}