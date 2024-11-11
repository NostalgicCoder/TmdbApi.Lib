namespace MovieTvTracker.Web.Models
{
    public class Stats
    {
        public List<FilmYear> FilmYears { get; set; }
        public List<FilmGenre> FilmGenres { get; set; }

        public Stats()
        {
            FilmYears = new List<FilmYear>();
            FilmGenres = new List<FilmGenre>();
        }
    }
}