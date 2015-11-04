using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChimetDataAnalysis
{
    /// <summary>
    /// Regular polls all weather stations.
    /// </summary>
    class StationPoller
    {
        public event EventHandler<NewRecordEventArgs> NewRecord;

        SouthamptonVTSDataSource sotonmet = new SouthamptonVTSDataSource(SouthamptonVTSStation.Sotonmet);
        SouthamptonVTSDataSource bramble = new SouthamptonVTSDataSource(SouthamptonVTSStation.Bramblemet);

        public StationPoller(System.Threading.CancellationToken cancellationToken)
        {
            this.StartRegularDataCheck(cancellationToken);
        }

        private void StartRegularDataCheck(System.Threading.CancellationToken cancellationToken)
        {
            // kick off update task
            Task.Run(async () =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    await CheckSotonmetAsync();
                    await CheckBramblemetAsync();

                    var brambleRecord = bramble.GetLatestDataPointAsync();

                    await Task.Delay(60000);
                }

                cancellationToken.ThrowIfCancellationRequested();
            }, cancellationToken);
        }

        private async Task CheckBramblemetAsync()
        {
            string stationID = "Bramblemet";
            SouthamptonVTSDataSource vtsSource = this.bramble;

            try
            {
                var record = await vtsSource.GetLatestDataPointAsync();
                this.NewRecord?.Invoke(this, new NewRecordEventArgs(record, stationID));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError("Exception while fetching latest data from '{0}'.", stationID);
                System.Diagnostics.Trace.TraceError("{0}", ex);
            }
        }

        private async Task CheckSotonmetAsync()
        {
            string stationID = "Sotonmet";
            SouthamptonVTSDataSource vtsSource = this.sotonmet;

            try
            {
                var record = await vtsSource.GetLatestDataPointAsync();
                this.NewRecord?.Invoke(this, new NewRecordEventArgs(record, stationID));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError("Exception while fetching latest data from '{0}'.", stationID);
                System.Diagnostics.Trace.TraceError("{0}", ex);
            }
        }
    }
}
