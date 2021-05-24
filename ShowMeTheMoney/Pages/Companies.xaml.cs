using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

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
            //CompanyListView.ItemsSource = Database.Database.Get<StockCompany>("Companies");

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
            //LoadingPanel.Children.Add(new Spinner());
        }

        /// <summary>
        ///     Event for running tasks in the background
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            //Database.Database.ClearCollection("Companies");
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
            // CompanyListView.ItemsSource = Database.Database.Get<StockCompany>("Companies");
        }

        private void CompanyListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //var comp = (StockCompany) e.AddedItems[0];
            //CompanyTitle.Text = comp.CompanyName;
            //try
            //{
            //    CompanyLogo.Source = new BitmapImage(new Uri(comp.CompanyLogo, UriKind.Absolute));
            //}
            //catch (Exception exception)
            //{
            //    Console.WriteLine(exception);
            //}

            //CompanyTicker.Text = comp.CompanyTicker;
            //CompanyAsk.Text = comp.PriceData.Ask.ToString(CultureInfo.InvariantCulture);
            //CompanySector.Text = comp.CompanySector;
            //CompanyPe.Text = comp.PriceData.TrailingPe.ToString(CultureInfo.InvariantCulture);
            //Company52H.Text = comp.PriceData.FiftyTwoWeekHigh.ToString(CultureInfo.InvariantCulture);
            //Company52L.Text = comp.PriceData.FiftyTwoWeekLow.ToString(CultureInfo.InvariantCulture);
            //Company50A.Text = comp.PriceData.FiftyDayAverage.ToString(CultureInfo.InvariantCulture);
        }
    }
}