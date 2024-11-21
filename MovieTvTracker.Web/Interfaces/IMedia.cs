using MovieTvTracker.Web.Models;
using TmdbApi.Lib.Models;

namespace MovieTvTracker.Web.Interfaces
{
    public interface IMedia
    {
        Int32 SelectedTMDBId { get; set; }
        string SelectedContentType { get; set; }
        string Keyword { get; set; }
        bool EnglishResultOnly { get; set; }
        ResultReturn TMDBData { get; set; }
        WatchedMediaResults WatchedMediaResults { get; set; }
        Stats Stats { get; set; }
        List<ResultReturn> FavoriteActorResults { get; set; }
    }
}