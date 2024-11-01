using TmdbApi.Lib.Models;

namespace TestHarness.Visual.Models
{
    public class Media
    {
        public Int32 SelectedTMDBId { get; set; }
        public string SelectedContentType { get; set; }
        public string Keyword { get; set; }
        public ResultReturn TMDBData { get; set; }
        public WatchedMediaResults WatchedMediaResults { get; set; }
        public FavoriteActorResults FavoriteActorResults { get; set; }
    }
}