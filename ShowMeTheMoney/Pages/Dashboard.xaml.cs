using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using LiveCharts;
using LiveCharts.Wpf;
using ShowMeTheMoney.CompanyBuilder.Models;
using ShowMeTheMoney.Events;
using ShowMeTheMoney.Helpers;
using ShowMeTheMoney.StockAnalyzer;
using ShowMeTheMoney.StockAnalyzer.Models;

namespace ShowMeTheMoney.Pages
{
    /// <summary>
    ///     Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Page
    {
        private readonly List<Portfolio> _portfolio;

        public Dashboard()
        {
            InitializeComponent();
            TbCompaniesInDb.Text = Database.Database.Get<Company>("Companies").Count.ToString();
            TbAnalyzedStocksInDb.Text = Database.Database.Get<Company>("AnalyzedStocks").Count.ToString();
            TbPortfolioStocksInDb.Text = Database.Database.Get<Company>("Portfolio").Count.ToString();
            _portfolio = Database.Database.Get<Portfolio>("Portfolio");
            PortfolioListView.ItemsSource = _portfolio;

            GetExpoLinesSeries.Series(_portfolio, out var lines);

            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Price",
                    Values = new ChartValues<double>(lines)
                }
            };
            DataContext = this;

            TbPortfolioStocksGainsInDb.Text = GetEarnings.Gainers(_portfolio);
            TbPortfolioStocksLossesInDb.Text = GetEarnings.Losses(_portfolio);
        }

        public SeriesCollection SeriesCollection { get; set; }

        public event EventHandler<Navigate> Navigate;

        /// <summary>
        ///     Event handler for invoke navigate
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnNavigate(Navigate e)
        {
            Navigate?.Invoke(this, e);
        }

        /// <summary>
        ///     Event handler for onClick for company list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CompanyList_OnClick_OnClick(object sender, RoutedEventArgs e)
        {
            OnNavigate(new Navigate("CompanyList"));
        }

        private void StocksList_OnClick_OnClick(object sender, RoutedEventArgs e)
        {
            OnNavigate(new Navigate("StockList"));
        }

        private void RefreshPortfolio_OnClick(object sender, RoutedEventArgs e)
        {
            if (_portfolio != null)
                StockRotate.Rotate(Database.Database.Get<Portfolio>("Portfolio"));
            PortfolioListView.ItemsSource = Database.Database.Get<Portfolio>("Portfolio");
            PortfolioListView.UpdateLayout();
        }
    }
}