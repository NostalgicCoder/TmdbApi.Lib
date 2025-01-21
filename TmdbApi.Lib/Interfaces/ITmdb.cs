using TmdbApi.Lib.Models;

namespace TmdbApi.Lib.Interfaces
{
    public interface ITmdb
    {
        void GetMovieGenreList();
        void GetTvGenreList();
        void GetConfigurationData();
        Rootobject CallTmdbApi(string query);
        ResultReturn SearchForFilmTvPerson(string keyword);
        Rootobject SearchForFilm(string keyword);
        Rootobject SearchForTv(string keyword);
        ResultReturn SearchForFilmAndCreditsById(int id);
        Rootobject SearchForFilmById(int id);
        Rootobject SearchForCreditsByFilmId(int id);
        ResultReturn SearchForTvAndCreditsById(int id);
        Rootobject SearchForTvById(int id);
        Rootobject SearchForCreditsByTvId(int id);
        void GetMovieImages(int id);
        ResultReturn MoviesNowPlaying();
        ResultReturn SearchForPersonAndCreditsById(int id);
        Rootobject SearchForPersonById(int id);
        Rootobject SearchForCombinedCreditsByPersonId(int id);
    }
}