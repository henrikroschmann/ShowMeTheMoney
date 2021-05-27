using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using ShowMeTheMoney.CompanyBuilder.Models;
using ShowMeTheMoney.Core;
using static Newtonsoft.Json.JsonConvert;

namespace ShowMeTheMoney.Pages
{
    /// <summary>
    ///     Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Page
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void ExportPortfolio_OnClick(object sender, RoutedEventArgs e)
        {
            var port = Database.Database.Get<Company>("Portfolio");
            // Configure save file dialog box
            var dlg = new SaveFileDialog
            {
                FileName = "Portfolio"
            };

            var result = dlg.ShowDialog();
            if (result != true) return;
            File.WriteAllText(dlg.FileName, SerializeObject(port));
        }

        private void ImportPortfolio_OnClick(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog
            {
                FileName = "Portfolio"
            };
            var result = dlg.ShowDialog();
            if (result != true) return;
            var files = File.ReadAllText(dlg.FileName);
            try
            {
                Database.Database.ClearCollection("Portfolio");
                foreach (var company in DeserializeObject<List<Company>>(files))
                    Database.Database.Create("Portfolio", company);
            }
            catch (Exception)
            {
                Logger.WriteEvent("Cannot restore Portfolio");
            }
        }
    }
}