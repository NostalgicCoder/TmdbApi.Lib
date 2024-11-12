namespace MovieTvTracker.Web.Interfaces
{
    public interface IGetStatistics
    {
        IMedia GetPopularYearsAndGenres(IMedia media);
    }
}