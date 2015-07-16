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
        public async Task<Dictionary<string, IEnumerable<double>>> DownloadData(DateTime day)
        {
            WebRequest dateRequest = HttpWebRequest.Create(string.Format("http://www.sotonmet.co.uk/archive/2015/July/CSV/Sot{0:ddMMMyyyy}.csv", day));

            WebResponse data = await dateRequest.GetResponseAsync();

            using (var reader = new System.IO.StreamReader(data.GetResponseStream()))
            {
                string content = await reader.ReadToEndAsync();
            }

            return null;
        }
    }
}
