using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using OxyPlot.Series;
using ChimetDataAnalysis.Data;

namespace ChimetDataAnalysis
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        StationPoller poller;

        public MainWindow()
        {
            this.InitializeComponent();

            this.plotView.Axes.Add(new OxyPlot.Wpf.DateTimeAxis()
            {
                Position = OxyPlot.Axes.AxisPosition.Bottom,
                IntervalType = OxyPlot.Axes.DateTimeIntervalType.Minutes, MajorGridlineStyle = OxyPlot.LineStyle.Automatic
            });

            this.plotView.Axes.Add(new OxyPlot.Wpf.LinearAxis()
            {
                Position = OxyPlot.Axes.AxisPosition.Left,
                LabelFormatter = p => string.Format("{0}kn", p), 
                MajorGridlineStyle = OxyPlot.LineStyle.Automatic, 
                MinorGridlineStyle = OxyPlot.LineStyle.Dot, 
                MinorStep = 1
            });

            this.DatePicker_Day.SelectedDate = DateTime.Now.AddDays(-1);
            this.Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await this.PopulateData();
            this.poller = new StationPoller(System.Threading.CancellationToken.None);

            StartRegularDataCheck(System.Threading.CancellationToken.None);
            this.poller.NewRecord += (sender, args) => System.Diagnostics.Debug.WriteLine("Record received from {0}", new[] { args.StationID });
        }

        private void StartRegularDataCheck(System.Threading.CancellationToken cancellationToken)
        {
            Task.Run(async () =>
        {
                var sotonmet = new SouthamptonVTSDataSource(SouthamptonVTSStation.Dockhead);
                var bramble = new SouthamptonVTSDataSource(SouthamptonVTSStation.Bramble);

                while (!cancellationToken.IsCancellationRequested)
                {
                    var sotonRecord = sotonmet.GetLatestDataPoint();
                    var brambleRecord = bramble.GetLatestDataPoint();

                    await Task.Delay(60000).ConfigureAwait(false);
                }

                cancellationToken.ThrowIfCancellationRequested();
            }, cancellationToken);
        }

        private async Task PopulateData()
        {
            this.IsEnabled = false;
            try
            {
                await this.PopulateData(this.DatePicker_Day.SelectedDate.Value, (Station)this.ComboBox_WeatherStation.SelectedItem);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                this.IsEnabled = true;
            }
        }

        private async Task PopulateData(DateTime day, Station station)
        {
            // test that 'station' is not null and throw an exception. 
            if (station == null)
            {
                throw new ArgumentNullException("station");
            }

            ChimetDataProvider dataDownloader = new ChimetDataProvider(station);

            IEnumerable<ChimetDataRecord> data = await dataDownloader.DownloadData(day);

            this.LineSeries_Average.ItemsSource = data
                .Where(record => record.AverageWindSpeed != null)
                .Select(record => new OxyPlot.DataPoint(OxyPlot.Axes.DateTimeAxis.ToDouble(record.Time), (double)record.AverageWindSpeed)).ToArray();

            this.LineSeries_Gust.ItemsSource = data
                .Where(record => record.MaximumWindSpeed != null)
                .Select(record => new OxyPlot.DataPoint(OxyPlot.Axes.DateTimeAxis.ToDouble(record.Time), (double)record.MaximumWindSpeed)).ToArray();
        }

        private async void ButtonApply_Click(object sender, RoutedEventArgs e)
        {
            await this.PopulateData();
        }
    }
}
