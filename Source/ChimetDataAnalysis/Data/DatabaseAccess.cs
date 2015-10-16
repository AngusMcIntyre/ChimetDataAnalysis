using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChimetDataAnalysis.Data
{
    /// <summary>
    /// Provides methods to interact with the weather database
    /// </summary>
    class DatabaseAccess
    {
        /// <summary>
        /// Asseses whether data has been downloaded for the specified day.
        /// </summary>
        /// <param name="day">The day to check for.</param>
        /// <returns></returns>
        public bool HasDataForDay(DateTime day)
        {
            using (var context = new WeatherDataDataContext())
            {
                // check if there is data for today
                return context.WeatherEntries.Where(entry => entry.Timestamp.Date == day.Date).Any();
            }
        }

        public IEnumerable<ChimetDataRecord> GetDataForDay(DateTime day)
        {
            using (var context = new WeatherDataDataContext())
            {
                // get the data from the data base
                return (from entry in context.WeatherEntries.Where(entry => entry.Timestamp.Date == day.Date)
                        select new ChimetDataRecord // map to chimet data record
                        {
                            AverageWindSpeed = entry.WindSpeed,
                            MaximumWindSpeed = entry.WindGust,
                            Time = entry.Timestamp,
                            WindBearing = entry.WindBearing
                        }).AsEnumerable();
            }
        }

        /// <summary>
        /// Inserts the specified records to the database.
        /// </summary>
        /// <param name="records">The records to add.</param>
        public void AddRecords(IEnumerable<ChimetDataRecord> records)
        {
            if(records == null)
            {
                throw new ArgumentNullException(nameof(records));
            }
        }
    }
}
