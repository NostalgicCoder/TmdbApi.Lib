using TmdbApi.Lib.Models;

namespace TestHarness.Visual.Models
{
    public class Media
    {
        public Int32 Id { get; set; }
        public string Type { get; set; }
        public string Keyword { get; set; }
        public ResultReturn TMDBData { get; set; }
    }
}