namespace MovieTvTracker.Web.Models
{
    public class Stats
    {
        public List<FilmYear> FilmYears { get; set; }
        public List<ContentGenre> FilmGenres { get; set; }
        public List<ContentGenre> TvGenres { get; set; }

        public Stats()
        {
            FilmYears = new List<FilmYear>();
            FilmGenres = new List<ContentGenre>();
            TvGenres = new List<ContentGenre>();
        }
    }
}