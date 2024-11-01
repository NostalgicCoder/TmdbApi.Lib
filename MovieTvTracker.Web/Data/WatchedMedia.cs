using System.ComponentModel.DataAnnotations;

namespace MovieTvTracker.Web.Data
{
    public class WatchedMedia
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int TMDBId { get; set; }
        public string ContentType { get; set; }
        public int WatchedQty { get; set; }
        public DateTime FirstWatched { get; set; }
        public DateTime LastWatched { get; set; }
    }
}