using MovieTvTracker.Web.Interfaces;
using TmdbApi.Lib.Models;

namespace MovieTvTracker.Web.Models
{
    public class Media : IMedia
    {
        public List<ResultReturn> FavoriteActorResults { get; set; }
        public ResultReturn TMDBData { get; set; }
        public WatchedMediaResults WatchedMediaResults { get; set; }
        public Stats Stats { get; set; }
        public Int32 SelectedTMDBId { get; set; }
        public Int32 FilmsThisYear { get; set; }
        public Int32 TvThisYear { get; set; }
        public Int32 FilmsThisMonth { get; set; }
        public Int32 FilmsLastMonth { get; set; }
        public Int32 TvThisMonth { get; set; }
        public Int32 TvLastMonth { get; set; }
        public string YearRange { get; set; }
        public string SelectedContentType { get; set; }
        public string Keyword { get; set; }
        public bool EnglishResultOnly { get; set; }

        public Media()
        {
            FavoriteActorResults = new List<ResultReturn>();
            Stats = new Stats();
            EnglishResultOnly = true;
        }
    }
}