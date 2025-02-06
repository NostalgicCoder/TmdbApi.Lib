using FluentAssertions;
using TmdbApi.Lib.Interfaces;
using TmdbApi.Lib.Models;

namespace TmdbApi.Lib.Tests
{
    [TestClass]
    public sealed class TmdbTests
    {
        private ITmdb _tmdb = new Tmdb();

        [TestMethod]
        public void CallCallTmdbApiShouldBeNull()
        {
            Rootobject result = _tmdb.CallTmdbApi(string.Empty);

            result.Should().BeNull();
        }

        [TestMethod]
        public void CallCallTmdbApiLookForRoboCopResultShouldMatchExpected()
        {
            string query = "/search/movie?query=Robocop&include_adult=false&language=en-US&page=1";

            Rootobject result = _tmdb.CallTmdbApi(query);

            result.results[0].original_title.Should().Be("RoboCop");
            result.results[0].release_date.Should().Be("1987-07-17");
        }

        [TestMethod]
        public void CallCallTmdbApiLookForFakeMovieShouldMatchZero()
        {
            string query = "/search/movie?query=FakeMovie&include_adult=false&language=en-US&page=1";

            Rootobject result = _tmdb.CallTmdbApi(query);

            result.results.Count().Should().Be(0);
        }

        [TestMethod]
        public void CallSearchForFilmTvPersonResultShouldMatchExpected()
        {
            string keyword = "RoboCop";

            ResultReturn result = _tmdb.SearchForFilmTvPerson(keyword);

            result.FilmResults.results[0].original_title.Should().Be("RoboCop");
            result.FilmResults.results[0].release_date.Should().Be("1987-07-17");

            result.TvResults.results[0].name.Should().Be("RoboCop: The Series");
            result.TvResults.results[0].first_air_date.Should().Be("1994-03-12");
        }

        [TestMethod]
        public void CallSearchForTvAndCreditsByIdResultShouldMatchExpected()
        {
            int id = 93219;

            ResultReturn result = _tmdb.SearchForTvAndCreditsById(id);

            result.TvIdResult.name.Should().Be("The Movies That Made Us"); 
            result.CreditsByTvId.id.Should().Be(93219);
        }
    }
}