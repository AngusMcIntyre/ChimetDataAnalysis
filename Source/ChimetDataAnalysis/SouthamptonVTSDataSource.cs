using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using flow = System.Threading.Tasks.Dataflow;

namespace ChimetDataAnalysis
{
    /// <summary>
    /// Fetches data from the Southampton VTS website.
    /// </summary>
    class SouthamptonVTSDataSource
    {
        public SouthamptonVTSDataSource(flow.ITargetBlock<ChimetDataRecord> target)
        {
            this.GetData();
        }

        private void GetData()
        {
            string fileModifiedDataUriFormat = "http://www.southamptonvts.co.uk//BackgroundSite/Ajax/FileModifiedDate?filePath={0}&w={1}";
            string loadXmlWithTransformUriFormat = "http://www.southamptonvts.co.uk//BackgroundSite/Ajax/LoadXmlFileWithTransform?xmlFilePath={0}&xslFilePath={1}&w={2}";

            string requestUri = string.Format(fileModifiedDataUriFormat, "D:\\ftp\\southampton\\Sotonmet.xml", DateTime.Now.Second);
            System.Net.WebRequest request = System.Net.WebRequest.Create(requestUri);


            DateTime timeStamp;

            using (var reader = new System.IO.StreamReader(request.GetResponse().GetResponseStream()))
            {
                timeStamp = DateTime.Parse(reader.ReadToEnd());
            }

            requestUri = string.Format(
                loadXmlWithTransformUriFormat,
                "D:\\ftp\\southampton\\Sotonmet.xml",
                "D:\\wwwroot\\CMS_Southampton\\content\\files\\assets\\SotonSnapshotmet.xsl",
                DateTime.Now.Second);

            request = System.Net.WebRequest.Create(requestUri);

            // load response as xml
            System.Xml.Linq.XDocument doc;
            using (var reader = new System.IO.StreamReader(request.GetResponse().GetResponseStream()))
            {
                doc = System.Xml.Linq.XDocument.Load(reader);
            }

            double averageSpeed;
            double gustSpeed;
            double bearing;

            // get the element with the average wind speed in it
            var averageSpeedElement = doc.Root.Elements("tr").Skip(1).First().Elements("td").Skip(1).First();

            if (!TryParseDouble(averageSpeedElement.Value, out averageSpeed))
            {
                return;
            }

            // get the element with the top wind speed in it
            var gustSpeedElement = doc.Root.Elements("tr").Skip(2).First().Elements("td").Skip(1).First();

            if (!TryParseDouble(gustSpeedElement.Value, out gustSpeed))
            {
                return;
            }

            // get the element with the wind bearing in it
            var bearingElement = doc.Root.Elements("tr").Skip(3).First().Elements("td").Skip(1).First();

            if (!TryParseDouble(bearingElement.Value, out bearing))
            {
                return;
            }

            // build the record
            ChimetDataRecord record = new ChimetDataRecord()
            {
                AverageWindSpeed = averageSpeed,
                MaximumWindSpeed = gustSpeed,
                WindBearing = bearing,
                Time = timeStamp
            };
        }

        private bool TryParseDouble(string value, out double result)
        {
            string number = System.Text.RegularExpressions.Regex.Match(value, @"^\d+.\d+").Value;
            return double.TryParse(number, out result);
        }
    }
}
