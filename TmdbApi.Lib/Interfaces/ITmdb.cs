using TmdbApi.Lib.Models;

namespace TmdbApi.Lib.Interfaces
{
    public interface ITmdb
    {
        void GetConfigurationData();
        void GetMovieImages(int id);
        Rootobject GetMovieGenreList();
        Rootobject GetTvGenreList();
        Rootobject CallTmdbApi(string query);
        Rootobject SearchForFilm(string keyword);
        Rootobject SearchForTv(string keyword);
        Rootobject SearchForFilmById(int id);
        Rootobject SearchForCreditsByFilmId(int id);
        Rootobject SearchForTvById(int id);
        Rootobject SearchForCreditsByTvId(int id);
        Rootobject SearchForPersonById(int id);
        Rootobject SearchForCombinedCreditsByPersonId(int id);
        ResultReturn MoviesNowPlaying();
        ResultReturn SearchForPersonAndCreditsById(int id);
        ResultReturn SearchForFilmTvPerson(string keyword);
        ResultReturn SearchForTvAndCreditsById(int id);
        ResultReturn SearchForFilmAndCreditsById(int id);
    }
}