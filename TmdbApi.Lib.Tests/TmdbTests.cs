using FakeItEasy;
using FluentAssertions;
using TmdbApi.Lib.Interfaces;
using TmdbApi.Lib.Models;

namespace TmdbApi.Lib.Tests
{
    [TestClass]
    public sealed class TmdbTests
    {
        private readonly ITmdb _tmdb;

        public TmdbTests()
        {
            // SUT (System Under Test)
            _tmdb = new Tmdb("YOUR_TMDB_API_READACCESS_TOKEN_GOES_HERE");
            //_tmdb = A.Fake<ITmdb>();
        }

        [TestMethod]
        public void Tmdb_CallTmdbApi_ShouldBeNull()
        {
            // Act
            Rootobject result = _tmdb.CallTmdbApi(string.Empty);

            // Assert
            result.Should().BeNull();
        }

        [TestMethod]
        public void Tmdb_CallTmdbApi_ShouldMatchExpectedResult()
        {
            // Arrange
            string query = "/search/movie?query=Robocop&include_adult=false&language=en-US&page=1";
            
            //A.CallTo(() => _tmdb.CallTmdbApi(query)).Returns(true);

            // Act
            Rootobject result = _tmdb.CallTmdbApi(query);

            // Assert
            result.Should().BeOfType<Rootobject>();
            result.results[0].original_title.Should().Be("RoboCop");
            result.results[0].release_date.Should().Be("1987-07-17");
        }

        [TestMethod]
        public void Tmdb_CallTmdbApi_ShouldHaveResultOfZeroAndNotBeNull()
        {
            // Arrange
            string query = "/search/movie?query=FakeMovie&include_adult=false&language=en-US&page=1";

            // Act
            Rootobject result = _tmdb.CallTmdbApi(query);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Rootobject>();
            result.results.Count().Should().Be(0);
        }

        [TestMethod]
        public void Tmdb_SearchForFilmTvPerson_ShouldMatchExpectedResult()
        {
            // Arrange
            string keyword = "RoboCop";

            // Act
            ResultReturn result = _tmdb.SearchForFilmTvPerson(keyword);

            // Assert
            result.Should().BeOfType<ResultReturn>();

            result.FilmResults.results[0].original_title.Should().Be("RoboCop");
            result.FilmResults.results[0].release_date.Should().Be("1987-07-17");

            result.TvResults.results[0].name.Should().Be("RoboCop: The Series");
            result.TvResults.results[0].first_air_date.Should().Be("1994-03-12");
        }

        [TestMethod]
        public void Tmdb_SearchForTvAndCreditsById_ShouldMatchExpectedResult()
        {
            // Arrange
            int id = 93219;

            // Act
            ResultReturn result = _tmdb.SearchForTvAndCreditsById(id);

            // Assert
            result.Should().BeOfType<ResultReturn>();
            result.TvIdResult.name.Should().Be("The Movies That Made Us");
            result.CreditsByTvId.id.Should().Be(93219);
        }
    }
}