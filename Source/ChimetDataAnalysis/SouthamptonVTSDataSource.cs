using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using flow = System.Threading.Tasks.Dataflow;

namespace ChimetDataAnalysis
{
    public enum SouthamptonVTSStation
    {
        Sotonmet,
        Bramblemet
    }

    /// <summary>
    /// Fetches data from the Southampton VTS website.
    /// </summary>
    class SouthamptonVTSDataSource
    {
        SouthamptonVTSStation station;

        public SouthamptonVTSDataSource(SouthamptonVTSStation station)
        {
            this.station = station;
        }

        /// <summary>
        /// Gets a <see cref="ChimetDataRecord"/> insance containing hthe latest data from the specified Southampton VTS site.
        /// </summary>
        /// <returns></returns>
        public async Task<ChimetDataRecord> GetLatestDataPointAsync()
        {
            try
            {
                string requestUri = this.GetLatestTimeRequestUri();
                System.Net.WebRequest request = System.Net.WebRequest.Create(requestUri);

                DateTime timeStamp;

                using (var reader = new System.IO.StreamReader((await request.GetResponseAsync()).GetResponseStream()))
                {
                    timeStamp = DateTime.Parse(reader.ReadToEnd());
                }

                requestUri = this.GetSnapshotRequestUri();

                request = System.Net.WebRequest.Create(requestUri);

                // load response as xml
                System.Xml.Linq.XDocument doc;

                using (var reader = new System.IO.StreamReader((await request.GetResponseAsync()).GetResponseStream()))
                {
                    doc = System.Xml.Linq.XDocument.Load(reader);
                }

                // get the element with the average wind speed in it
                var averageSpeedElement = doc.Root.Elements("tr").Skip(1).First().Elements("td").Skip(1).First();

                // get the element with the top wind speed in it
                var gustSpeedElement = doc.Root.Elements("tr").Skip(2).First().Elements("td").Skip(1).First();

                // get the element with the wind bearing in it
                var bearingElement = doc.Root.Elements("tr").Skip(3).First().Elements("td").Skip(1).First();

                // build the record
                ChimetDataRecord record = new ChimetDataRecord()
                {
                    AverageWindSpeed = ParseDouble(averageSpeedElement.Value),
                    MaximumWindSpeed = ParseDouble(gustSpeedElement.Value),
                    WindBearing = ParseDouble(bearingElement.Value),
                    Time = timeStamp
                };

                return record;
            }
            catch (Exception ex)
            {
                throw new DataDownloadedException(ex);
            }
        }

        /// <summary>
        /// Gets a <see cref="System.Uri"/> for requesting the timestamp of the laterst reading.
        /// </summary>
        /// <returns></returns>
        private string GetLatestTimeRequestUri()
        {
            string fileModifiedDataUriFormat = "http://www.southamptonvts.co.uk//BackgroundSite/Ajax/FileModifiedDate?filePath={0}&w={1}";
            return string.Format(fileModifiedDataUriFormat, GetFileUri(this.station), DateTime.Now.Second);
        }

        /// <summary>
        /// Gets the path the data file on the server.
        /// </summary>
        /// <param name="station">The station that the data will be requested for.</param>
        /// <returns></returns>
        private static string GetFileUri(SouthamptonVTSStation station)
        {
            switch (station)
            {
                case SouthamptonVTSStation.Bramblemet:
                    return "D:\\ftp\\southampton\\Bramble.xml";

                case SouthamptonVTSStation.Sotonmet:
                default:
                    return "D:\\ftp\\southampton\\Sotonmet.xml";
            }
        }

        /// <summary>
        /// Gets a <see cref="System.Uri"/> for requesting the the laterst reading.
        /// </summary>
        /// <returns></returns>
        private string GetSnapshotRequestUri()
        {
            string loadXmlWithTransformUriFormat = "http://www.southamptonvts.co.uk//BackgroundSite/Ajax/LoadXmlFileWithTransform?xmlFilePath={0}&xslFilePath={1}&w={2}";

            return string.Format(
                loadXmlWithTransformUriFormat,
                GetFileUri(this.station),
                "D:\\wwwroot\\CMS_Southampton\\content\\files\\assets\\SotonSnapshotmet.xsl",
                DateTime.Now.Second);
        }

        private double ParseDouble(string value)
        {
            string number = System.Text.RegularExpressions.Regex.Match(value, @"^\d+(.\d+)?").Value;
            return double.Parse(number);
        }
    }
}
