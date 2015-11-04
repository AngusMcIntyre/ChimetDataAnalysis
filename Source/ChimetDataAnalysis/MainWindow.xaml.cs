﻿using System;
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

            this.DatePicker_Day.SelectedDateChanged += DatePicker_Day_SelectedDateChanged;
            this.ComboBox_WeatherStation.SelectionChanged += ComboBox_WeatherStation_SelectionChanged;
            this.Loaded += MainWindow_Loaded;

            this.poller = new StationPoller(System.Threading.CancellationToken.None);

            this.poller.NewRecord += (sender, args) => System.Diagnostics.Debug.WriteLine("Record received from {0}", new[] { args.StationID });
        }

        private async void ComboBox_WeatherStation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            await this.PopulateData();
        }

        private async void DatePicker_Day_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            await this.PopulateData();
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await this.PopulateData();
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
                // log and swallow this exception
                System.Diagnostics.Trace.TraceError("An exception occured while populating historic data for {0} from {1}.", this.DatePicker_Day.SelectedDate.Value, ((Station)this.ComboBox_WeatherStation.SelectedItem).Address);
                System.Diagnostics.Trace.TraceError("{0}", ex);

                // clear all data to make it clear that nothing was downloaded.
                this.LineSeries_Average.ItemsSource = null;
                this.LineSeries_Gust.ItemsSource = null;
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

        private void Button_PreviousDay_Click(object sender, RoutedEventArgs e)
        {
            this.DatePicker_Day.SelectedDate = this.DatePicker_Day.SelectedDate.Value.AddDays(-1);
        }

        private void Button_NextDay_Click(object sender, RoutedEventArgs e)
        {
            this.DatePicker_Day.SelectedDate = this.DatePicker_Day.SelectedDate.Value.AddDays(1);
        }

        private void Button_GoToToday_Click(object sender, RoutedEventArgs e)
        {
            this.DatePicker_Day.SelectedDate = DateTime.Now.Date;
        }
    }
}
