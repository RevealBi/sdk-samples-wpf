using DataSource_InMemory.Data;
using DataSource_InMemory.Providers;
using Reveal.Sdk;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace DataSource_InMemory
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;

            var salesData = SalesDataGenerator.GenerateSales(10000);
            RevealSdkSettings.DataProvider = new InMemoryDataProvider(salesData);           
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _revealView.DataSourcesRequested += RevealView_DataSourcesRequested;

            var filePath = Path.Combine(Environment.CurrentDirectory, "Dashboards/SalesWithGlobalFilter.rdash");
            _revealView.Dashboard = new RVDashboard(filePath);

        }

        private void RevealView_DataSourcesRequested(object sender, DataSourcesRequestedEventArgs e)
        {
            List<RVDashboardDataSource> datasources = new List<RVDashboardDataSource>();
            List<RVDataSourceItem> datasourceItems = new List<RVDataSourceItem>();

            var inMemoryDataSourceItem = new RVInMemoryDataSourceItem("SalesRecords")
            {
                Title = "Sales Records"                
            };

            datasourceItems.Add(inMemoryDataSourceItem);

            e.Callback(new RevealDataSources(datasources, datasourceItems, true));
        }
    }
}
