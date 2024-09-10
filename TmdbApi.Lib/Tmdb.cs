using Newtonsoft.Json;
using RestSharp;
using TmdbApi.Lib.Class;
using TmdbApi.Lib.Models;

namespace TmdbApi.Lib
{
    // https://developer.themoviedb.org/reference/intro/getting-started

    public class Tmdb
    {
        private RestClient _client = new RestClient("https://api.themoviedb.org/3");

        private List<string> _logoSizes;
        private List<string> _posterSizes;

        private string _readAccessToken = "PRIVATE";
        private string _imgPath;

        public Rootobject CallTmdbApi(string query)
        {
            RestRequest request = new RestRequest(query);
            request.AddHeader("Authorization", "Bearer " + _readAccessToken);

            RestResponse response = _client.Execute(request, Method.Get);

            Rootobject result = new Rootobject();

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string rawResponse = response.Content;

                return JsonConvert.DeserializeObject<Rootobject>(rawResponse);
            }

            return null;
        }

        /// <summary>
        /// Aquire information from the configuration endpoint
        /// - Data on image URL filepaths, sizing etc
        /// </summary>
        public void GetConfigurationData()
        {
            string query = Endpoint.Configuration;

            Rootobject result = CallTmdbApi(query);

            if (result.images != null)
            {
                _imgPath = result.images.secure_base_url;
                _logoSizes = result.images.logo_sizes.ToList();
                _posterSizes = result.images.poster_sizes.ToList();
            }
        }

        /// <summary>
        /// Search the Movie/TV endpoint for results that match the keyword
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public ResultReturn SearchForFilmTV(string keyword)
        {
            ResultReturn resultReturn = new ResultReturn();

            resultReturn.FilmResults = SearchForFilm(keyword);
            resultReturn.TVResults = SearchForTv(keyword);

            return resultReturn;
        }

        /// <summary>
        /// Search the Movie endpoint for results that match the keyword
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public Rootobject SearchForFilm(string keyword)
        {
            string query = Endpoint.SearchMovie + keyword + "&include_adult=false&language=en-US&page=1";

            Rootobject result = CallTmdbApi(query);

            return result;
        }

        /// <summary>
        /// Search the Movie endpoint for a specific result that matches the id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResultReturn SearchForFilmById(int id)
        {
            ResultReturn resultReturn = new ResultReturn();

            string query = Endpoint.SearchMovieId + id;

            resultReturn.FilmIdResult = CallTmdbApi(query);

            return resultReturn;
        }

        /// <summary>
        /// Search the TV endpoint for results that match the keyword
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public Rootobject SearchForTv(string keyword)
        {
            string query = Endpoint.SearchTV + keyword + "&include_adult=false&language=en-US&page=1";

            Rootobject result = CallTmdbApi(query);

            return result;
        }

        /// <summary>
        /// - https://developer.themoviedb.org/docs/image-basics
        /// - https://www.themoviedb.org/talk/5f3ef4eec175b200365ee352
        /// </summary>
        /// <param name="movieId"></param>
        public void GetMovieImages(int movieId)
        {
            string query = "/movie/" + movieId + "/images";

            Rootobject result = CallTmdbApi(query);

            string logo = _imgPath + _logoSizes.Last<string>() + result.logos.FirstOrDefault().file_path;
            string url = _imgPath + _posterSizes.Last<string>() + result.posters.FirstOrDefault().file_path;
        }
    }
}