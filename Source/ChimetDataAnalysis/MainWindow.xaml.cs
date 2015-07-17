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

namespace ChimetDataAnalysis
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
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
            this.PopulateData();
        }

        async Task PopulateData()
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

            var data = await dataDownloader.DownloadData(day);

            this.LineSeries_Average.ItemsSource = data.Select(record => new OxyPlot.DataPoint(OxyPlot.Axes.DateTimeAxis.ToDouble(record.Time), record.AverageWindSpeed)).ToArray();
            this.LineSeries_Gust.ItemsSource = data.Select(record => new OxyPlot.DataPoint(OxyPlot.Axes.DateTimeAxis.ToDouble(record.Time), record.MaximumWindSpeed)).ToArray();
        }

        private void ButtonApply_Click(object sender, RoutedEventArgs e)
        {
            this.PopulateData();
        }
    }
}
