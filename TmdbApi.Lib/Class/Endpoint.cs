namespace TmdbApi.Lib.Class
{
    public class Endpoint
    {
        public const string Configuration = "/configuration";
        public const string SearchMovie = "/search/movie?query=";
        public const string SearchMovieId = "/movie/";
        public const string SearchTVId = "/tv/";
        public const string SearchTV = "/search/tv?query=";
        public const string MoviesNowPlaying = "/movie/now_playing?language=en-US&page=1&region=GB";
        public const string Languages = "/configuration/languages";
        public const string Countries = "/configuration/countries";

        // The below endpoints need to be tagged onto the end of a 'SearchMovieId' request alongside a valid movieId
        public const string Providers = "/watch/providers";
        public const string Credits = "/credits";
        public const string MovieImages = "/images";
    }
}