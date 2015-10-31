using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChimetDataAnalysis
{
    /// <summary>
    /// Occurs when there was a fault downloading data.
    /// </summary>
    class DataDownloadedException : Exception
    {
        private Exception ex;

        public DataDownloadedException(Exception ex) : base("A fault occured while downloading data.", ex)
        {
        }
    }
}
