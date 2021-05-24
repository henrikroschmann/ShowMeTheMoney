using ShowMeTheMoney.StockAnalyzer.Models;
using System;
using System.Windows;

namespace ShowMeTheMoney
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Logging
            Logger.WriteEvent += WriteEventHandler;

            Logger.WriteLine("Hello Logger has been initialized");
        }

        #region LogInformation

        private void WriteEventHandler(string message)
        {
            /// To handle event from other treads we need dispatching. 
            LogOutput.Dispatcher.BeginInvoke(new Action(() => LogOutput.AppendText(message)));
        }

        #endregion LogInformation
        #region Buttons

        /// <summary>
        /// Event for handling movement of window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnCTLetButtonDownEvent(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            DragMove();
        }

        private void Overview_OnClick_OnClick(object sender, RoutedEventArgs e)
        {
        }

        private void StockAnalyzer_OnClick_OnClick(object sender, RoutedEventArgs e)
        {
            ContentFrame.Content = "";
        }

        /// <summary>
        /// Load Company page in frame
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CompanyList_OnClick_OnClick(object sender, RoutedEventArgs e)
        {
            ContentFrame.Content = new Pages.Companies();
        }

        private void Settings_OnClick_OnClick_OnClick(object sender, RoutedEventArgs e)
        {
            ContentFrame.Content = "";
        }

        private void QuitApplication_OnClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        #endregion Buttons
    }
}