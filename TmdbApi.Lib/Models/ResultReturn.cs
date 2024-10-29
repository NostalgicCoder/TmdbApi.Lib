namespace TmdbApi.Lib.Models
{
    public class ResultReturn
    {
        public Rootobject FilmResults { get; set; }
        public Rootobject TVResults { get; set; }
        public Rootobject PersonResults { get; set; }
        public Rootobject FilmIdResult { get; set; }
        public Rootobject TVIdResult { get; set; }
        public Rootobject MoviesNowPlaying { get; set; }
        public Rootobject CreditsByFilmId { get; set; }
        public Rootobject CreditsByTvId { get; set; }

        public ResultReturn()
        {
            FilmResults = new Rootobject();
            TVResults = new Rootobject();
            PersonResults = new Rootobject();
            FilmIdResult = new Rootobject();
            TVIdResult = new Rootobject();
            MoviesNowPlaying = new Rootobject();
            CreditsByFilmId = new Rootobject();
            CreditsByTvId = new Rootobject();
        }
    }
}