namespace TmdbApi.Lib.Class
{
    public class Endpoint
    {
        public const string SearchMovie = "/search/movie?query=";
        public const string SearchMovieId = "/movie/";
        public const string SearchTv = "/search/tv?query=";
        public const string SearchTvId = "/tv/";
        public const string SearchPerson = "/search/person?query=";
        public const string SearchPersonId = "/person/";
        public const string MoviesNowPlaying = "/movie/now_playing?language=en-US&page=1&region=GB";
        public const string Languages = "/configuration/languages";
        public const string Countries = "/configuration/countries";
        public const string Configuration = "/configuration";
        public const string MovieGenres = "/genre/movie/list";
        public const string TvGenres = "/genre/tv/list";

        // The below endpoints need to be tagged onto the end of a '/movie/' or '/tv/' or '/person/' request alongside a valid TMDB id
        public const string Providers = "/watch/providers";
        public const string Credits = "/credits";
        public const string MovieImages = "/images";
        public const string PersonCombinedCredits = "/combined_credits";
        public const string PersonMovieCredits = "/movie_credits";
        public const string PersonTvCredits = "/tv_credits";
    }
}