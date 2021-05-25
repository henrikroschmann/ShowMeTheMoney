using ShowMeTheMoney.CompanyBuilder.Models;
using ShowMeTheMoney.Events;
using System;
using System.Windows;
using System.Windows.Controls;

namespace ShowMeTheMoney.Pages
{
    /// <summary>
    ///     Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Page
    {
        public Dashboard()
        {
            InitializeComponent();
            TbCompaniesInDb.Text = Database.Database.Get<Company>("Companies").Count.ToString();
            TbAnalyzedStocksInDb.Text = Database.Database.Get<Company>("AnalyzedStocks").Count.ToString();
            TbPortfolioStocksInDb.Text = Database.Database.Get<Company>("Portfolio").Count.ToString();
            PortfolioListView.ItemsSource = Database.Database.Get<Company>("Portfolio");
        }

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
    }
}