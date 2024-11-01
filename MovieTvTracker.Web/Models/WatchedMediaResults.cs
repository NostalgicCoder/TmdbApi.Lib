namespace MovieTvTracker.Web.Models
{
    public class WatchedMediaResults
    {
        public List<WatchedMediaItem> WatchedFilms { get; set; }
        public List<WatchedMediaItem> WatchedTV { get; set; }

        public WatchedMediaResults()
        {
            WatchedFilms = new List<WatchedMediaItem>();
            WatchedTV = new List<WatchedMediaItem>();
        }
    }
}