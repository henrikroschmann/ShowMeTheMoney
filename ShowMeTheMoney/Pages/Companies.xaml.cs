using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using ShowMeTheMoney.CompanyBuilder.Models;
using ShowMeTheMoney.Helpers;
using ShowMeTheMoney.UserControl;

namespace ShowMeTheMoney.Pages
{
    /// <summary>
    ///     Interaction logic for Companies.xaml
    /// </summary>
    public partial class Companies : Page
    {
        private readonly BackgroundWorker _worker = new();

        /// <summary>
        ///     Constructor initialize components, populate company list and subscribe to events.
        /// </summary>
        public Companies()
        {
            InitializeComponent();
            CompanyListView.ItemsSource = Database.Database.Get<Company>("Companies");

            _worker.DoWork += Worker_DoWork;
            _worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
        }

        /// <summary>
        ///     Click event for regenerate button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RegenerateCompanyList_OnClick(object sender, RoutedEventArgs e)
        {
            _worker.RunWorkerAsync();
            CompanyListView.Visibility = Visibility.Hidden;
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
            Database.Database.ClearCollection("Companies");
            CompanyBuilder.CompanyBuilder.GetCompanies();
        }

        /// <summary>
        ///     Event that runs after the worker has completed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            LoadingPanel.Visibility = Visibility.Hidden;
            CompanyListView.Visibility = Visibility.Visible;
            CompanyListView.ItemsSource = Database.Database.Get<Company>("Companies");
        }

        private void CompanyListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comp = (Company) e.AddedItems[0];
            if (comp == null) return;
            CompanyTitle.Text = comp.Name;
            try
            {
                CompanyLogo.Source =
                    new BitmapImage(new Uri(GetCompanyLogo.Get(comp.Name), UriKind.Absolute));
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }

            CompanyTicker.Text = comp.Symbol;
            CompanyAsk.Text = comp.PriceData.Ask.ToString(CultureInfo.InvariantCulture);
            CompanySector.Text = comp.Sector;
            //CompanyPe.Text = comp.PriceData.TrailingPe.ToString(CultureInfo.InvariantCulture);
            Company52H.Text = comp.PriceData.FiftyTwoWeekHigh.ToString(CultureInfo.InvariantCulture);
            Company52L.Text = comp.PriceData.FiftyTwoWeekLow.ToString(CultureInfo.InvariantCulture);
            Company50A.Text = comp.PriceData.FiftyDayAverage.ToString(CultureInfo.InvariantCulture);
        }
    }
}