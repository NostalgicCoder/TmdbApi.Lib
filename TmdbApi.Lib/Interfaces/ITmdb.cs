using TmdbApi.Lib.Models;

namespace TmdbApi.Lib.Interfaces
{
    public interface ITmdb
    {
        Rootobject CallTmdbApi(string query);
        void GetConfigurationData();
        ResultReturn SearchForFilmTV(string keyword);
        Rootobject SearchForFilm(string keyword);
        Rootobject SearchForTv(string keyword);
        ResultReturn SearchForFilmAndCreditsById(int id);
        Rootobject SearchForFilmById(int id);
        Rootobject SearchForCreditsByFilmId(int id);
        ResultReturn SearchForTVById(int id);
        void GetMovieImages(int id);
        ResultReturn MoviesNowPlaying();
    }
}