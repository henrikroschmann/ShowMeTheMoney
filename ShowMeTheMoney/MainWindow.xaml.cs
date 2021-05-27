using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ShowMeTheMoney.Core;
using ShowMeTheMoney.Events;
using ShowMeTheMoney.Pages;

namespace ShowMeTheMoney
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Dashboard _dashboard;

        public MainWindow()
        {
            InitializeComponent();

            _dashboard = new Dashboard();
            _dashboard.Navigate += OnNavigate;
            ContentFrame.Content = _dashboard;
            // Logging
            Logger.WriteEvent += WriteEventHandler;
            Logger.WriteLine("Logger initialized");
        }

        #region Events

        private void OnNavigate(object sender, Navigate e)
        {
            ContentFrame.Content = e.Destination switch
            {
                "CompanyList" => new Companies(),
                "StockList" => new StockList(),
                _ => ContentFrame.Content
            };
        }

        #endregion Events

        #region LogInformation

        /// <summary>
        ///     Write to the logger
        ///     To handle event from other treads we need dispatching.
        /// </summary>
        /// <param name="message"></param>
        private void WriteEventHandler(string message)
        {
            LogOutput.Dispatcher.BeginInvoke(new Action(() => LogOutput.AppendText(message)));
        }

        /// <summary>
        ///     Text change event for to show the most recent log
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
        ///     Event for handling movement of window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnCTLetButtonDownEvent(object sender, MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            DragMove();
        }

        private void Overview_OnClick_OnClick(object sender, RoutedEventArgs e)
        {
            ContentFrame.Content = _dashboard;
        }

        private void StockAnalyzer_OnClick_OnClick(object sender, RoutedEventArgs e)
        {
            ContentFrame.Content = new StockList();
        }

        /// <summary>
        ///     Load Company page in frame
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CompanyList_OnClick_OnClick(object sender, RoutedEventArgs e)
        {
            ContentFrame.Content = new Companies();
        }

        /// <summary>
        ///     Load settings page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Settings_OnClick_OnClick_OnClick(object sender, RoutedEventArgs e)
        {
            ContentFrame.Content = new Settings();
        }

        /// <summary>
        ///     Exit the application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QuitApplication_OnClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        #endregion Buttons
    }
}