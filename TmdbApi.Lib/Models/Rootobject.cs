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
        public bool adult { get; set; }
        public string backdrop_path { get; set; }
        public Belongs_To_Collection belongs_to_collection { get; set; }
        public int budget { get; set; }
        public Genre[] genres { get; set; }
        public string homepage { get; set; }
        public string imdb_id { get; set; }
        public string[] origin_country { get; set; }
        public string original_language { get; set; }
        public string original_title { get; set; }
        public string overview { get; set; }
        public float popularity { get; set; }
        public string poster_path { get; set; }
        public Production_Companies[] production_companies { get; set; }
        public Production_Countries[] production_countries { get; set; }
        public string release_date { get; set; }
        public int revenue { get; set; }
        public int runtime { get; set; }
        public Spoken_Languages[] spoken_languages { get; set; }
        public string status { get; set; }
        public string tagline { get; set; }
        public string title { get; set; }
        public bool video { get; set; }
        public float vote_average { get; set; }
        public int vote_count { get; set; }
        public Dates dates { get; set; }
        public Created_By[] created_by { get; set; }
        public object[] episode_run_time { get; set; }
        public string first_air_date { get; set; }
        public bool in_production { get; set; }
        public string[] languages { get; set; }
        public string last_air_date { get; set; }
        public Last_Episode_To_Air last_episode_to_air { get; set; }
        public string name { get; set; }
        public object next_episode_to_air { get; set; }
        public Network[] networks { get; set; }
        public int number_of_episodes { get; set; }
        public int number_of_seasons { get; set; }
        public string original_name { get; set; }
        public Season[] seasons { get; set; }
        public string type { get; set; }
        public Cast[] cast { get; set; }
        public Crew[] crew { get; set; }
        public string[] also_known_as { get; set; }
        public string biography { get; set; }
        public string birthday { get; set; }
        public object deathday { get; set; }
        public int gender { get; set; }
        public string known_for_department { get; set; }
        public string place_of_birth { get; set; }
        public string profile_path { get; set; }
    }
}