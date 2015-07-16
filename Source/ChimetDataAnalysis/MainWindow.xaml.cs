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

namespace ChimetDataAnalysis
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.PopulateData();
        }

        private async void PopulateData()
        {
            await this.DownloadData(DateTime.Now.AddDays(-1));
        }

        private async Task DownloadData(DateTime day)
        {
            ChimetDataProvider dataDownloader = new ChimetDataProvider();

            var data = await dataDownloader.DownloadData(day);
        }
    }
}
