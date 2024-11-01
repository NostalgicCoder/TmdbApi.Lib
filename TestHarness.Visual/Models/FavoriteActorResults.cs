using MovieTvTracker.Web.Data;

namespace MovieTvTracker.Web.Models
{
    public class FavoriteActorResults
    {
        public List<FavoriteActor> FavoriteActors { get; set; }

        public FavoriteActorResults()
        {
            FavoriteActors = new List<FavoriteActor>();
        }
    }
}