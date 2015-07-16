using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChimetDataAnalysis
{
    class ChimetDataRecord
    {
        public DateTime Time { get; set; }
        public double AverageWindSpeed { get; set; }
        public double WindBearing { get; set; }
        public double MaximumWindSpeed { get; set; }
    }
}
