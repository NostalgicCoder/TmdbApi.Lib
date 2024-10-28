using Newtonsoft.Json;
using RestSharp;
using TmdbApi.Lib.Class;
using TmdbApi.Lib.Interfaces;
using TmdbApi.Lib.Models;

namespace TmdbApi.Lib
{
    public class Tmdb : ITmdb
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

                try
                {
                    return JsonConvert.DeserializeObject<Rootobject>(rawResponse);
                }
                catch (Exception ex)
                {
                    return null;
                }
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
        public ResultReturn SearchForFilmTv(string keyword)
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

            return CallTmdbApi(query);
        }

        /// <summary>
        /// Search the TV endpoint for results that match the keyword
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public Rootobject SearchForTv(string keyword)
        {
            string query = Endpoint.SearchTV + keyword + "&include_adult=false&language=en-US&page=1";

            return CallTmdbApi(query);
        }









        /// <summary>
        /// Search for film and credits by TMDB id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResultReturn SearchForFilmAndCreditsById(int id)
        {
            ResultReturn resultReturn = new ResultReturn();

            resultReturn.FilmIdResult = SearchForFilmById(id);
            resultReturn.CreditsByFilmId = SearchForCreditsByFilmId(id);

            return resultReturn;
        }

        /// <summary>
        /// Search for film by TMDB id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Rootobject SearchForFilmById(int id)
        {
            string query = Endpoint.SearchMovieId + id;

            return CallTmdbApi(query);
        }

        /// <summary>
        /// Search for film credits by TMDB id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Rootobject SearchForCreditsByFilmId(int id)
        {
            string query = Endpoint.SearchMovieId + id + Endpoint.Credits;

            return CallTmdbApi(query);
        }

        /// <summary>
        /// Search for tv series and credits by TMDB id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResultReturn SearchForTvAndCreditsById(int id)
        {
            ResultReturn resultReturn = new ResultReturn();

            resultReturn.TVIdResult = SearchForTvById(id);
            resultReturn.CreditsByTvId = SearchForCreditsByTvId(id);

            return resultReturn;
        }

        /// <summary>
        /// Search for tv series by TMDB id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Rootobject SearchForTvById(int id)
        {
            string query = Endpoint.SearchTVId + id;

            return CallTmdbApi(query);
        }

        /// <summary>
        /// Search for tv series credits by TMDB id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Rootobject SearchForCreditsByTvId(int id)
        {
            string query = Endpoint.SearchTVId + id + Endpoint.Credits;

            return CallTmdbApi(query);
        }

        /// <summary>
        /// Gets URL paths and logo and poster sizing information for films
        /// - https://developer.themoviedb.org/docs/image-basics
        /// - https://www.themoviedb.org/talk/5f3ef4eec175b200365ee352
        /// </summary>
        /// <param name="movieId"></param>
        public void GetMovieImages(int id)
        {
            string query = Endpoint.SearchMovieId + id + Endpoint.MovieImages;

            Rootobject result = CallTmdbApi(query);

            string logo = _imgPath + _logoSizes.Last<string>() + result.logos.FirstOrDefault().file_path;
            string url = _imgPath + _posterSizes.Last<string>() + result.posters.FirstOrDefault().file_path;
        }

        /// <summary>
        /// Returns a list of films currently showing in cinemas in the UK
        /// </summary>
        /// <returns></returns>
        public ResultReturn MoviesNowPlaying()
        {
            ResultReturn resultReturn = new ResultReturn();

            string query = Endpoint.MoviesNowPlaying;

            resultReturn.MoviesNowPlaying = CallTmdbApi(query);

            return resultReturn;
        }
    }
}