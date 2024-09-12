namespace TmdbApi.Lib.Models
{
    public class ResultReturn
    {
        public Rootobject FilmResults { get; set; }
        public Rootobject TVResults { get; set; }
        public Rootobject FilmIdResult { get; set; }
        public Rootobject TVIdResult { get; set; }
        public Rootobject MoviesNowPlaying { get; set; }
    }
}