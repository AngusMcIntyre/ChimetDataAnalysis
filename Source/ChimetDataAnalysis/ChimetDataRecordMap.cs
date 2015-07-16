using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChimetDataAnalysis
{
    class ChimetDataRecordMap: CsvHelper.Configuration.CsvClassMap<ChimetDataRecord>
    {
        public ChimetDataRecordMap()
        {
            this.Map(record => record.Time).ConvertUsing(row => row.GetField<DateTime>("Date").Add(row.GetField<TimeSpan>("Time")));
            this.Map(record => record.AverageWindSpeed).Name("WSPD");
            this.Map(record => record.MaximumWindSpeed).Name("GST");
            this.Map(record => record.WindBearing).Name("WD");
        }
    }
}
