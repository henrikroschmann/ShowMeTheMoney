using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using LiveCharts;
using LiveCharts.Wpf;
using ShowMeTheMoney.Core;
using ShowMeTheMoney.StockAnalyzer;
using ShowMeTheMoney.UserControl;

namespace ShowMeTheMoney.Pages
{
    /// <summary>
    ///     Interaction logic for StockList.xaml
    /// </summary>
    public partial class StockList : Page
    {
        private readonly BackgroundWorker _worker = new();
        private StockAnalyzer.Models.StockList _selectedStock;
        /// <summary>
        ///     Constructor initialize components, populate company list and subscribe to events.
        /// </summary>
        public StockList()
        {
            InitializeComponent();
            StocksListView.ItemsSource = GetCompanyStock.Get().Where(x => x.Stock != null);

            _worker.DoWork += Worker_DoWork;
            _worker.RunWorkerCompleted += Worker_RunWorkerCompleted;

            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Price",
                    Values = new ChartValues<double>(new double[] {0, 0, 0})
                }
            };
            YFormatter = value => value.ToString("C");
            DataContext = this;
        }

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }

        /// <summary>
        ///     Click event for regenerate button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FindGoodStocks_OnClick(object sender, RoutedEventArgs e)
        {
            _worker.RunWorkerAsync();
            StocksListView.Visibility = Visibility.Hidden;
            LoadingPanel.Visibility = Visibility.Visible;
            LoadingPanel.Children.Add(new Spinner());
        }

        /// <summary>
        ///     Event for running tasks in the background
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            GetStocks.Get();
        }

        /// <summary>
        ///     Event that runs after the worker has completed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            LoadingPanel.Visibility = Visibility.Hidden;
            StocksListView.Visibility = Visibility.Visible;
            StocksListView.ItemsSource = GetCompanyStock.Get().Where(x => x.Stock != null);
        }

        private void StocksListView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedStock = (StockAnalyzer.Models.StockList) e.AddedItems[0];
            CompanyTitle.Text = _selectedStock?.Name;
            SeriesCollection[0].Values =
                new ChartValues<double>(_selectedStock.Stock.TimeSeries.Close.Reverse());
            Labels = _selectedStock.Stock.TimeSeries.Time.Reverse().Select(x => x.Day.ToString()).ToArray();


            DailyTrend.Text = _selectedStock.Stock.DailyTrend switch
            {
                0 => "Neural",
                1 => "Positive",
                -1 => "Negative",
                _ => DailyTrend.Text
            };

            DailyBuySignalFound.Text = _selectedStock.Stock.DailyBuySignalFound switch
            {
                0 => "Neural",
                1 => "Positive",
                -1 => "Negative",
                _ => DailyTrend.Text
            };

            DailySuperTrend.Text = _selectedStock.Stock.DailySuperTrend switch
            {
                0 => "Neural",
                1 => "Positive",
                -1 => "Negative",
                _ => DailyTrend.Text
            };

            DailyMacDSignalFound.Text = _selectedStock.Stock.DailyMacDSignalFound switch
            {
                0 => "Neural",
                1 => "Positive",
                -1 => "Negative",
                _ => DailyTrend.Text
            };
        }

        private void SaveToPortfolio_OnClick(object sender, RoutedEventArgs e)
        {
            Logger.WriteLine($"Added {_selectedStock.Name} to portfolio");
            Database.Database.Create("Portfolio", _selectedStock);            
        }
    }
}