using TmdbApi.Lib.Models;

namespace TestHarness.Visual.Models
{
    public class WatchedMediaItem
    {
        public ResultReturn ResultReturn { get; set; }
        public WatchedMedia WatchedMedia { get; set; }

        public WatchedMediaItem() 
        {
            ResultReturn = new ResultReturn();
            WatchedMedia = new WatchedMedia();  
        }
    }
}