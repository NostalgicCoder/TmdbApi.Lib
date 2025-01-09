namespace MovieTvTracker.Web.Interfaces
{
    public interface IGetStatistics
    {
        IMedia GetFilmYearsRangeAndGenres(IMedia media);
        IMedia GetTvGenres(IMedia media);
        IMedia GetQtyViewingStats(IMedia media);
    }
}