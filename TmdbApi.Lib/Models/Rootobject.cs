namespace TmdbApi.Lib.Models
{
    public class Rootobject
    {
        public int page { get; set; }
        public Result[] results { get; set; }
        public int total_pages { get; set; }
        public int total_results { get; set; }
        public Backdrop[] backdrops { get; set; }
        public int id { get; set; }
        public Logo[] logos { get; set; }
        public Poster[] posters { get; set; }
        public Images images { get; set; }
        public string[] change_keys { get; set; }
    }
}