using System.ComponentModel.DataAnnotations;

namespace TestHarness.Visual.Models
{
    public class WatchedMedia
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public Int32 TMDBId { get; set; }
        public string ContentType { get; set; }
        public Int32 WatchedQty { get; set; }        
        public DateTime FirstWatched { get; set; }
        public DateTime LastWatched { get; set; }
    }
}