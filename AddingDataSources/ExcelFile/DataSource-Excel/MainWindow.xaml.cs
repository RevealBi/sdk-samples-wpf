using Reveal.Sdk;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace DataSource_Excel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            RevealSdkSettings.LocalDataFilesRootFolder = Path.Combine(Environment.CurrentDirectory, "Data");

            _revealView.DataSourcesRequested += RevealView_DataSourcesRequested;
        }

        private void RevealView_DataSourcesRequested(object sender, DataSourcesRequestedEventArgs e)
        {
            var dataSources = new List<RVDashboardDataSource>();
            var items = new List<RVDataSourceItem>();

            var localFileItem = new RVLocalFileDataSourceItem();
            localFileItem.Uri = $"local:/Samples.xlsx"; // "local:/" is a required prefix

            var excelDataSourceItem = new RVExcelDataSourceItem(localFileItem);
            excelDataSourceItem.Title = "Local Excel File";
            items.Add(excelDataSourceItem);

            e.Callback(new RevealDataSources(dataSources, items, true));
        }
    }
}
