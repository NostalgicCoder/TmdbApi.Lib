using System.ComponentModel.DataAnnotations;

namespace MovieTvTracker.Web.Data
{
    public class FavoriteActor
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int TMDBId { get; set; }
    }
}