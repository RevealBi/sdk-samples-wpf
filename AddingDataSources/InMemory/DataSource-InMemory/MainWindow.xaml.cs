using DataSource_InMemory.Data;
using DataSource_InMemory.Providers;
using Reveal.Sdk;
using System.Collections.Generic;
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

            var salesData = SalesDataGenerator.GenerateSales(10000);
            RevealSdkSettings.DataProvider = new InMemoryDataProvider(salesData);
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
