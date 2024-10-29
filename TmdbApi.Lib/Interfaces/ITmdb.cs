using TmdbApi.Lib.Models;

namespace TmdbApi.Lib.Interfaces
{
    public interface ITmdb
    {
        Rootobject CallTmdbApi(string query);
        void GetConfigurationData();
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
    }
}