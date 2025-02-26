namespace TmdbApi.Lib.Models
{
    public class ResultReturn
    {
        public Rootobject FilmResults { get; set; }
        public Rootobject TvResults { get; set; }
        public Rootobject PersonResults { get; set; }
        public Rootobject FilmIdResult { get; set; }
        public Rootobject TvIdResult { get; set; }
        public Rootobject PersonIdResult { get; set; }
        public Rootobject MoviesNowPlaying { get; set; }
        public Rootobject CreditsByFilmId { get; set; }
        public Rootobject CreditsByTvId { get; set; }
        public Rootobject CombinedCreditsByPersonId { get; set; }
        public string KnownForMovies { get; set; }
        public string PersonDobAge { get; set; }
        public string Score { get; set; }

        public ResultReturn()
        {
            FilmResults = new Rootobject();
            TvResults = new Rootobject();
            PersonResults = new Rootobject();
            FilmIdResult = new Rootobject();
            TvIdResult = new Rootobject();
            PersonIdResult = new Rootobject();
            MoviesNowPlaying = new Rootobject();
            CreditsByFilmId = new Rootobject();
            CreditsByTvId = new Rootobject();
            CombinedCreditsByPersonId = new Rootobject();
        }
    }
}