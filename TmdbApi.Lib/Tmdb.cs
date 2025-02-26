using Newtonsoft.Json;
using RestSharp;
using TmdbApi.Lib.Class;
using TmdbApi.Lib.Interfaces;
using TmdbApi.Lib.Models;

namespace TmdbApi.Lib
{
    public class Tmdb : ITmdb
    {
        private IHelper _helper = new Helper();

        private RestClient _client;

        private readonly string _readAccessToken;

        private List<string> _logoSizes;
        private List<string> _posterSizes;

        private string _imgPath;

        public Tmdb(string readAccessToken)
        {
            _client = new RestClient("https://api.themoviedb.org/3");
            _readAccessToken = readAccessToken;
        }

        /// <summary>
        /// Call TMDB API with authorization token and request query, deserialize response into a recognised object type.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
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
                    throw new Exception("Unable to deserialize JSON to a object", ex.InnerException);
                }
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                // Usually caused when no '_readAccessToken' has been provided on the call
                throw new Exception("No valid TMDB API key provided");
            }
            else if (response.StatusCode == 0)
            {
                // Usually caused when there is no internet connection
                throw new Exception("No internet connection");
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
        /// Search the Movie/TV/Person endpoint for results that match the keyword
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public ResultReturn SearchForFilmTvPerson(string keyword)
        {
            ResultReturn resultReturn = new ResultReturn();

            resultReturn.FilmResults = SearchForFilm(keyword);
            resultReturn.TvResults = SearchForTv(keyword);
            resultReturn.PersonResults = SearchForPerson(keyword);

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
            string query = Endpoint.SearchTv + keyword + "&include_adult=false&language=en-US&page=1";

            return CallTmdbApi(query);
        }

        /// <summary>
        /// Search the person endpoint for results that match the keyword
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public Rootobject SearchForPerson(string keyword)
        {
            string query = Endpoint.SearchPerson + keyword + "&include_adult=false&language=en-US&page=1";

            return CallTmdbApi(query);
        }

        /// <summary>
        /// Search for film and credits by TMDB id
        /// - Reformat data from TMDB API result to format a friendly display score
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResultReturn SearchForFilmAndCreditsById(int id)
        {
            ResultReturn resultReturn = new ResultReturn();

            resultReturn.FilmIdResult = SearchForFilmById(id);
            resultReturn.CreditsByFilmId = SearchForCreditsByFilmId(id);

            if (resultReturn.FilmIdResult != null)
            {
                resultReturn.Score = string.Format("{0}% ({1} votes)", @Math.Round((resultReturn.FilmIdResult.vote_average * 10), 0), resultReturn.FilmIdResult.vote_count);
            }

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
        /// - Reformat data from TMDB API result to format a friendly display score
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResultReturn SearchForTvAndCreditsById(int id)
        {
            ResultReturn resultReturn = new ResultReturn();

            resultReturn.TvIdResult = SearchForTvById(id);
            resultReturn.CreditsByTvId = SearchForCreditsByTvId(id);

            if (resultReturn.TvIdResult != null)
            {
                resultReturn.Score = string.Format("{0}% ({1} votes)", @Math.Round((resultReturn.TvIdResult.vote_average * 10), 0), resultReturn.TvIdResult.vote_count);
            }

            return resultReturn;
        }

        /// <summary>
        /// Search for tv series by TMDB id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Rootobject SearchForTvById(int id)
        {
            string query = Endpoint.SearchTvId + id;

            return CallTmdbApi(query);
        }

        /// <summary>
        /// Search for tv series credits by TMDB id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Rootobject SearchForCreditsByTvId(int id)
        {
            string query = Endpoint.SearchTvId + id + Endpoint.Credits;

            return CallTmdbApi(query);
        }

        /// <summary>
        /// Search for person and their combined movie/tv credits and known for movies by TMDB id
        /// - Regarding 'KnownForMovies' - https://www.themoviedb.org/talk/51742ec8760ee3470c4bd73f
        /// - Also configures the birthday/deathday information to return a useful value for 'PersonDobAge'
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResultReturn SearchForPersonAndCreditsById(int id)
        {
            ResultReturn resultReturn = new ResultReturn();

            resultReturn.PersonIdResult = SearchForPersonById(id);
            resultReturn.CombinedCreditsByPersonId = SearchForCombinedCreditsByPersonId(id);
            // TMDB API doesnt have a API call to bring back movies a actor is known for, the below is the best way todo it currently but isnt perfect
            resultReturn.KnownForMovies = string.Join(", ", resultReturn.CombinedCreditsByPersonId.cast.Where(item => item.media_type == "movie").OrderByDescending(item => item.vote_count).Take(3).OrderByDescending(item => item.vote_average).Select(item => item.original_title).ToArray());
            resultReturn.PersonDobAge = _helper.ProcessDob(resultReturn.PersonIdResult);

            return resultReturn;
        }

        /// <summary>
        /// Search for person by TMDB id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Rootobject SearchForPersonById(int id)
        {
            string query = Endpoint.SearchPersonId + id;

            return CallTmdbApi(query);
        }

        /// <summary>
        /// Search for combined movie/tv credits for a person by TMDB id
        /// - Doesnt return 'Known for' movies/tv for a person
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Rootobject SearchForCombinedCreditsByPersonId(int id)
        {
            string query = Endpoint.SearchPersonId + id + Endpoint.PersonCombinedCredits;

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

        /// <summary>
        /// Returns the current list of available movie genres
        /// </summary>
        public Rootobject GetMovieGenreList()
        {
            string query = Endpoint.MovieGenres;

            Rootobject result = CallTmdbApi(query);

            return result;
        }

        /// <summary>
        /// Returns the current list of available TV genres
        /// </summary>
        public Rootobject GetTvGenreList()
        {
            string query = Endpoint.TvGenres;

            Rootobject result = CallTmdbApi(query);

            return result;
        }
    }
}