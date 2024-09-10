namespace TmdbApi.Lib.Class
{
    public class Endpoint
    {
        public const string Configuration = "/configuration";
        public const string SearchMovie = "/search/movie?query=";
        public const string SearchMovieId = "/movie/";
        public const string SearchTV = "/search/tv?query=";
        public const string MoviesNowPlaying = "/movie/now_playing?language=en-US&page=1&region=GB";
        public const string Languages = "/configuration/languages";
        public const string Countries = "/configuration/countries";
    }
}