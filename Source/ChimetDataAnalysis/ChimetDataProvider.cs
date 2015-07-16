using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace ChimetDataAnalysis
{
    class ChimetDataProvider
    {
        public string Site { get; set; }

        public Station Station { get; set; }

        public ChimetDataProvider(Station station)
        {
            this.Station = station;
        }

        public async Task<IEnumerable<ChimetDataRecord>> DownloadData(DateTime day)
        {
            WebRequest dateRequest = HttpWebRequest.Create(string.Format("http://{1}/archive/{0:yyyy}/{0:MMMM}/CSV/{2}{0:ddMMMyyyy}.csv", day, this.Station.Address, this.Station.ShortName));

            WebResponse data = await dateRequest.GetResponseAsync().ConfigureAwait(false);

            CsvHelper.Configuration.CsvConfiguration config = new CsvHelper.Configuration.CsvConfiguration();
            config.RegisterClassMap<ChimetDataRecordMap>();

            using (var reader = new CsvHelper.CsvReader(new System.IO.StreamReader(data.GetResponseStream()), config))
            {
                return reader.GetRecords<ChimetDataRecord>().ToList().AsReadOnly();
            }
        }
    }
}
