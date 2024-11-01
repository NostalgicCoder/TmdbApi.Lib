using TestHarness.Visual.Data;

namespace TestHarness.Visual.Models
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