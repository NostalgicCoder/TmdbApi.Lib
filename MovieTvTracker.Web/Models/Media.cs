using MovieTvTracker.Web.Interfaces;
using TmdbApi.Lib.Models;

namespace MovieTvTracker.Web.Models
{
    public class Media : IMedia
    {
        public Int32 SelectedTMDBId { get; set; }
        public string SelectedContentType { get; set; }
        public string Keyword { get; set; }
        public ResultReturn TMDBData { get; set; }
        public WatchedMediaResults WatchedMediaResults { get; set; }
        public Stats Stats { get; set; }
        public List<ResultReturn> FavoriteActorResults { get; set; }
        
        public Media()
        {
            FavoriteActorResults = new List<ResultReturn>();
            Stats = new Stats();    
        }
    }
}