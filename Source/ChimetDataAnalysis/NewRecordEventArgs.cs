namespace ChimetDataAnalysis
{
    public class NewRecordEventArgs
    {
        /// <summary>
        /// Initializes a new instance of <see cref="NewRecordEventArgs"/>.
        /// </summary>
        /// <param name="record">The new record.</param>
        /// <param name="stationID">The ID of the station that generated this record.</param>
        public NewRecordEventArgs(ChimetDataRecord record, string stationID)
        {
            this.Record = record;
            this.StationID = stationID;
        }

        public string StationID
        {
            get; private set;
        }

        public ChimetDataRecord Record
        {
            get; private set;
        }
    }
}