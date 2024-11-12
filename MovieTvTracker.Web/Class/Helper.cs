using MovieTvTracker.Web.Interfaces;

namespace MovieTvTracker.Web.Class
{
    public class Helper : IHelper
    {
        /// <summary>
        /// Get the birthday and deathday (If exists) of a person and calculate how old the person is in years and if dead on day of death, handle for bad data
        /// </summary>
        /// <param name="media"></param>
        /// <returns></returns>
        public string ProcessDob(IMedia media)
        {
            string msg = "Unknown";

            try
            {
                if (!string.IsNullOrEmpty(media.TMDBData.PersonIdResult.birthday))
                {
                    DateTime birthday = Convert.ToDateTime(media.TMDBData.PersonIdResult.birthday);

                    int ageSinceDob = CalcDuration(birthday, DateTime.Now);

                    msg = string.Format("{0} ({1} years old)", birthday.ToString("dd/MM/yyyy"), ageSinceDob);

                    if (media.TMDBData.PersonIdResult.deathday != null)
                    {
                        DateTime deathDay = Convert.ToDateTime(media.TMDBData.PersonIdResult.deathday);

                        int ageOnDeath = CalcDuration(birthday, deathDay);

                        msg = string.Format("{0} - {1} (Died at {2} years old, would have been {3} years old)", birthday.ToString("dd/MM/yyyy"), deathDay.ToString("dd/MM/yyyy"), ageOnDeath, ageSinceDob);
                    }
                }
            }
            catch(Exception ex)
            {
                // TODO: Add error handling
            }

            return msg;
        }

        /// <summary>
        /// Calculate the duration in years between a start and a enddate
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        private int CalcDuration(DateTime startDate, DateTime endDate)
        {
            DateTime zeroTime = new DateTime(1, 1, 1);

            TimeSpan span = endDate - startDate;

            return (zeroTime + span).Year - 1;
        }
    }
}