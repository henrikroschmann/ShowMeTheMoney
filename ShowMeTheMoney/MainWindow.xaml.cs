using ShowMeTheMoney.Core;
using System;
using System.Windows;
using System.Windows.Controls;

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

            ContentFrame.Content = new Pages.Dashboard();
            // Logging
            Logger.WriteEvent += WriteEventHandler;
            Logger.WriteLine("Logger initialized");
        }

        #region LogInformation

        /// <summary>
        /// Write to the logger
        /// </summary>
        /// <param name="message"></param>
        private void WriteEventHandler(string message)
        {
            /// To handle event from other treads we need dispatching.
            LogOutput.Dispatcher.BeginInvoke(new Action(() => LogOutput.AppendText(message)));
        }

        /// <summary>
        /// Text change event for to show the most recent log
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LogOutput_TextChanged(object sender, TextChangedEventArgs e)
        {
            LogOutput.ScrollToEnd();
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

        private void Overview_OnClick_OnClick(object sender, RoutedEventArgs e) => ContentFrame.Content = new Pages.Dashboard();

        private void StockAnalyzer_OnClick_OnClick(object sender, RoutedEventArgs e) => ContentFrame.Content = new Pages.StockList();

        /// <summary>
        /// Load Company page in frame
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CompanyList_OnClick_OnClick(object sender, RoutedEventArgs e) => ContentFrame.Content = new Pages.Companies();

        /// <summary>
        /// Load settings page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Settings_OnClick_OnClick_OnClick(object sender, RoutedEventArgs e) => ContentFrame.Content = new Pages.Settings();

        /// <summary>
        /// Exit the application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QuitApplication_OnClick(object sender, RoutedEventArgs e) => Application.Current.Shutdown();

        #endregion Buttons
    }
}