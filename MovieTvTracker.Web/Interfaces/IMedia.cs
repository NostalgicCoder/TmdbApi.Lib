using MovieTvTracker.Web.Models;
using TmdbApi.Lib.Models;

namespace MovieTvTracker.Web.Interfaces
{
    public interface IMedia
    {
        List<ResultReturn> FavoriteActorResults { get; set; }
        WatchedMediaResults WatchedMediaResults { get; set; }
        Stats Stats { get; set; }
        ResultReturn TMDBData { get; set; }
        Int32 SelectedTMDBId { get; set; }
        Int32 FilmsThisYear { get; set; }
        Int32 TvThisYear { get; set; }
        Int32 FilmsThisMonth { get; set; }
        Int32 FilmsLastMonth { get; set; }
        Int32 TvThisMonth { get; set; }
        Int32 TvLastMonth { get; set; }
        string YearRange { get; set; }
        string SelectedContentType { get; set; }
        string Keyword { get; set; }
        bool EnglishResultOnly { get; set; }
    }
}